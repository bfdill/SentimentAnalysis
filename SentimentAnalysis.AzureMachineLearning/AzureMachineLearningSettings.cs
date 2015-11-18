namespace SentimentAnalysis.AzureMachineLearning
{
    using System;
    using System.Configuration;
    using Domain;

    public class AzureMachineLearningSettings : IAzureMachingLearningSettings
    {
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

        public int GetBatchItemLimit()
        {
            var configValue = ConfigurationManager.AppSettings[Constants.BatchItemLimitConfigKey];

            if (configValue == null)
            {
                return Constants.DefaultBatchItemLimit;
            }

            int limit;

            if (!int.TryParse(configValue, out limit))
            {
                throw new InvalidCastException($"Invalid value for {Constants.BatchItemLimitConfigKey} configraution setting");
            }

            return limit;
        }

        public decimal GetNegativeSentimentThreshold()
        {
            var configValue = ConfigurationManager.AppSettings[Constants.NegativeSentimentThresholdConfigKey];

            if (configValue == null)
            {
                return Constants.DefaultNegativeSentimentThreshold;
            }

            decimal threshold;

            if (!decimal.TryParse(configValue, out threshold))
            {
                throw new InvalidCastException($"Invalid value for {Constants.NegativeSentimentThresholdConfigKey} configraution setting");
            }

            return threshold;
        }

        public decimal GetPositiveSentimentThreshold()
        {
            var configValue = ConfigurationManager.AppSettings[Constants.PositiveSentimentThresholdConfigKey];

            if (configValue == null)
            {
                return Constants.DefaultPositiveSentimentThreshold;
            }

            decimal threshold;

            if (!decimal.TryParse(configValue, out threshold))
            {
                throw new InvalidCastException($"Invalid value for {Constants.PositiveSentimentThresholdConfigKey} configraution setting");
            }

            return threshold;
        }

        public Uri GetServiceBaseUri()
        {
            var uri = ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] ?? Constants.DefaultServiceBaseUri;

            try
            {
                return new Uri(uri);
            }
            catch (UriFormatException)
            {
                throw new UriFormatException($"Unable to parse the {Constants.ServiceBaseUriConfigKey} setting into a valid URI: {uri}");
            }
        }
    }
}