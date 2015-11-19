namespace SentimentAnalysis.AzureMachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Domain;
    using Domain;
    using Newtonsoft.Json;
    using AMLSettings = Domain.IAzureMachingLearningSettings;

    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly IErrorMessageGenerator _errorMessageGenerator;

        private readonly ISentimentAnalysisRequestor _requestor;

        private readonly AMLSettings _settings;

        public SentimentAnalysisService(ISentimentAnalysisRequestor requestor, IErrorMessageGenerator errorMessageGenerator, AMLSettings settings)
        {
            if (requestor == null)
            {
                throw new ArgumentNullException(nameof(requestor));
            }

            if (errorMessageGenerator == null)
            {
                throw new ArgumentNullException(nameof(errorMessageGenerator));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _requestor = requestor;
            _errorMessageGenerator = errorMessageGenerator;
            _settings = settings;
        }

        public async Task<Result> GetSentimentAsync(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return (await GetBatchSentimentAsync(new Dictionary<string, string> { { string.Empty, text } })).First().Value;
        }

        public async Task<IDictionary<string, Result>> GetBatchSentimentAsync(Dictionary<string, string> textBatch)
        {
            ValidateBatchRequest(textBatch);

            if (!textBatch.Any())
            {
                return new Dictionary<string, Result>();
            }

            string content;
            using (var response = await _requestor.PostAsync(Constants.SentimentBatchRequest, BuildInputString(textBatch)))
            {
                content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return textBatch.ToDictionary(r => r.Key, r => AzureMachineLearningResult.Build(_errorMessageGenerator.GenerateError(response.StatusCode, content)));
                }
            }

            var result = JsonConvert.DeserializeObject<SentimentBatchResult>(content);
            var parsedResults = result.SentimentBatch.ToDictionary(sr => sr.Id, sr => AzureMachineLearningResult.Build(sr.Score, ScoreToSentiment(sr.Score)));

            foreach (var error in result.Errors)
            {
                parsedResults.Add(error.Id, AzureMachineLearningResult.Build(error.Message));
            }

            return parsedResults;
        }

        private static string BuildInputString(IDictionary<string, string> textBatch)
        {
            var i = new StringBuilder();
            i.Append("{\"Inputs\":[");

            foreach (var req in textBatch)
            {
                i.AppendFormat("{{\"Id\":\"{0}\",\"Text\":\"{1}\"}},", req.Key, req.Value);
            }

            i.Append("]}");

            return i.ToString();
        }

        private Sentiment ScoreToSentiment(decimal score)
        {
            if (score < 0 || score > 1)
            {
                return Sentiment.Invalid;
            }

            if (score < _settings.GetNegativeSentimentThreshold())
            {
                return Sentiment.Negative;
            }

            return score > _settings.GetPositiveSentimentThreshold() ? Sentiment.Positive : Sentiment.Neutral;
        }

        private void ValidateBatchRequest(Dictionary<string, string> textBatch)
        {
            if (textBatch == null)
            {
                throw new ArgumentNullException(nameof(textBatch));
            }

            if (textBatch.Count > _settings.GetBatchItemLimit())
            {
                throw new InvalidOperationException(Constants.BatchItemLimitExceededErrorText);
            }
        }
    }
}