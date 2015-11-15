namespace SentimentAnalysis.Core
{
    using System;
    using System.Configuration;
    using Domain;

    public class Settings : ISettings
    {
        private readonly Uri _defaultServiceBaseUri;

        public Settings()
        {
        }

        public Settings(Uri defaultServiceBaseUri)
        {
            _defaultServiceBaseUri = defaultServiceBaseUri;
        }

        public string GetBasicAuthenticationCredentials()
        {
            var key = ConfigurationManager.AppSettings[Constants.BasicAuthenticationCredentialsConfigKey];

            if (key == null)
            {
                throw new InvalidOperationException(
                    $@"You must specify basic authentication credentials in the form of username:password in the ({Constants.BasicAuthenticationCredentialsConfigKey}) app setting. ");
            }

            return key;
        }

        public Uri GetServiceBaseUri()
        {
            if (_defaultServiceBaseUri != null)
            {
                return _defaultServiceBaseUri;
            }

            var uri = ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey];

            try
            {
                return new Uri(uri);
            }
            catch (UriFormatException)
            {
                throw new UriFormatException($"Unable to parse the {Constants.ServiceBaseUriConfigKey} setting into a valid URI: {uri}");
            }
        }

        public int GetBatchLimit()
        {
            var configValue = ConfigurationManager.AppSettings[Constants.BatchLimitConfigKey];

            if (configValue == null)
            {
                return Constants.DefaultBatchLimit;
            }

            int limit;
            if (!int.TryParse(configValue, out limit))
            {
                throw new InvalidCastException($"Invalid value for {Constants.BatchLimitConfigKey} configraution setting");
            }

            return limit;
        }

        public int GetBatchByteSizeLimit()
        {
            var configValue = ConfigurationManager.AppSettings[Constants.BatchByteSizeLimitConfigKey];

            if (configValue == null)
            {
                return Constants.DefaultBatchByteSizeLimit;
            }

            int limit;
            if (!int.TryParse(configValue, out limit))
            {
                throw new InvalidCastException($"Invalid value for {Constants.BatchByteSizeLimitConfigKey} configraution setting");
            }

            return limit;
        }
    }
}