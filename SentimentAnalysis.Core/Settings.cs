﻿namespace SentimentAnalysis.Core
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

        public string GetApiKey()
        {
            var key = ConfigurationManager.AppSettings[Constants.AccountConfigKey];

            if (key == null)
            {
                throw new InvalidOperationException(
                    $@"You must specify an account key ({Constants.AccountConfigKey}) app setting. ");
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
    }
}