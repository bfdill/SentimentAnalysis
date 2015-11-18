namespace SentimentAnalysis.Vivekn
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Core.Domain;
    using Domain;

    public class SentimentAnalysisRequestor : ISentimentAnalysisRequestor
    {
        private readonly IViveknSettings _viveknSettings;

        public SentimentAnalysisRequestor(IViveknSettings viveknSettings)
        {
            if (viveknSettings == null)
            {
                throw new ArgumentNullException(nameof(viveknSettings));
            }

            _viveknSettings = viveknSettings;
        }

        public async Task<HttpResponseMessage> PostAsync(string request, string body)
        {
            using (var client = BuildClient())
            {
                using (var content = new StringContent(body))
                {
                    return await client.PostAsync(request, content);
                }
            }
        }

        private HttpClient BuildClient()
        {
            return new HttpClient { BaseAddress = _viveknSettings.GetServiceBaseUri() };
        }
    }
}