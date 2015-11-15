namespace SentimentAnalysis.Core
{
    using System;
    using System.Net.Http.Headers;
    using System.Text;
    using Domain;

    public class RequestHeaderFactory : IRequestHeaderFactory
    {
        private readonly ISettings _settings;

        public RequestHeaderFactory(ISettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _settings = settings;
        }

        public string AuthorizationHeader()
        {
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_settings.GetBasicAuthenticationCredentials()));
        }

        public MediaTypeWithQualityHeaderValue AcceptHeader()
        {
            return new MediaTypeWithQualityHeaderValue("application/json");
        }
    }
}