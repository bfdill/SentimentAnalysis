namespace SentimentAnalysis.Vivekn
{
    using System;
    using System.Configuration;
    using Domain;

    public class ViveknSettings : IViveknSettings
    {
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