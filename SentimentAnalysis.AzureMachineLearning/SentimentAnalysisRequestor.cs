namespace SentimentAnalysis.AzureMachineLearning
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Core;
    using Core.Domain;

    public class SentimentAnalysisRequestor : ISentimentAnalysisRequestor
    {
        private readonly IRequestHeaderFactory _headerFactory;

        private readonly ISettings _settings;

        public SentimentAnalysisRequestor(IRequestHeaderFactory headerFactory, ISettings settings)
        {
            if (headerFactory == null)
            {
                throw new ArgumentNullException(nameof(headerFactory));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _headerFactory = headerFactory;
            _settings = settings;
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

        public async Task<HttpResponseMessage> GetAsync(string request)
        {
            using (var client = BuildClient())
            {
                return await client.GetAsync(request);
            }
        }

        private HttpClient BuildClient()
        {
            var client = new HttpClient { BaseAddress = _settings.GetServiceBaseUri() };

            client.DefaultRequestHeaders.Add(Constants.AuthorizationHeaderName, _headerFactory.AuthorizationHeader());
            client.DefaultRequestHeaders.Accept.Add(_headerFactory.AcceptHeader());

            return client;
        }
    }
}