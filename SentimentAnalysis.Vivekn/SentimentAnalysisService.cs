namespace SentimentAnalysis.Vivekn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

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

            if (!IsBatchSizeValid(fc))
            {
                return textBatch.ToDictionary(tb => tb.Key, tb => ViveknResult.Build(Constants.BatchByteSizeLimitExceededErrorText));
            }

            string rc;

            using (var response = await _requestor.PostAsync(Constants.SentimentBatchRequest, fc))
            {
                rc = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return textBatch.ToDictionary(tb => tb.Key, tb => ViveknResult.Build(_errorMessageGenerator.GenerateError(response.StatusCode, rc)));
                }
            }

            var results = (IEnumerable<dynamic>)JsonConvert.DeserializeObject(rc, typeof(IEnumerable<dynamic>));
            return textBatch
                .Keys
                .Zip(results, (key, result) => new Tuple<string, Result>(key, ResultFactory(result)))
                .ToDictionary(r => r.Item1, r => r.Item2);
        }

        private static Result ResultFactory(dynamic r)
        {
            return ViveknResult.Build((decimal)r.confidence, (Sentiment)Enum.Parse(typeof(Sentiment), (string)r.result, true));
        }

        private bool IsBatchSizeValid(string fc)
        {
            return Encoding.UTF8.GetByteCount(fc) < _settings.GetBatchByteSizeLimit();
        }
    }
}