namespace SentimentAnalysis.Vivekn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Domain;
    using Domain;
    using Newtonsoft.Json;

    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly IErrorMessageGenerator _errorMessageGenerator;

        private readonly ISentimentAnalysisRequestor _requestor;

        private readonly IViveknSettings _settings;

        public SentimentAnalysisService(ISentimentAnalysisRequestor requestor, IErrorMessageGenerator errorMessageGenerator, IViveknSettings settings)
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
            return (await GetBatchSentimentAsync(new Dictionary<string, string> { { string.Empty, text } })).First().Value;
        }

        public async Task<IDictionary<string, Result>> GetBatchSentimentAsync(Dictionary<string, string> textBatch)
        {
            if (textBatch == null)
            {
                throw new ArgumentNullException(nameof(textBatch));
            }

            if (!textBatch.Any())
            {
                return new Dictionary<string, Result>();
            }

            var values = textBatch.Select(b => b.Value?.ToLower());
            var fc = JsonConvert.SerializeObject(values);
            string rc;

            using (var response = await _requestor.PostAsync(Constants.SentimentBatchRequest, fc))
            {
                rc = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return textBatch.ToDictionary(tb => tb.Key, tb => (Result)ViveknResult.Build(_errorMessageGenerator.GenerateError(response.StatusCode, rc)));
                }
            }

            dynamic results = JsonConvert.DeserializeObject(rc);

            var keys = textBatch.Keys.ToArray();
            var output = new Dictionary<string, Result>(textBatch.Count);
            var i = 0;

            foreach (var r in results)
            {
                decimal confidence = r.confidence;
                string sentimentText = r.result;
                var sentiment = (Sentiment)Enum.Parse(typeof(Sentiment), sentimentText, true);
                var item = ViveknResult.Build(confidence, sentiment);

                // var item = ViveknResult.Build(decimal.Parse(r.confidence), (Sentiment)Enum.Parse(typeof(Sentiment), (string)r.result, true));

                output.Add(keys[i], item);

                i++;
            }

            return output;
        }
    }
}