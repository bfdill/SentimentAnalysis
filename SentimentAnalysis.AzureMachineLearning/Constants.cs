namespace SentimentAnalysis.AzureMachineLearning
{
    public class Constants
    {
        public const string BasicAuthenticationCredentialsConfigKey = "AzureMachineLearning:BasicAuthenticationCredentials";

        public const string BatchItemLimitConfigKey = "AzureMachineLearning:BatchItemLimit";

        public const string BatchItemLimitExceededErrorText = "The number of requests is greater than the batch limit.";

        public const int DefaultBatchItemLimit = 1000;

        public const decimal DefaultNegativeSentimentThreshold = 40m;

        public const decimal DefaultPositiveSentimentThreshold = 70m;

        public const string NegativeSentimentThresholdConfigKey = "AzureMachineLearning:NegativeSentimentThreshold";

        public const string PositiveSentimentThresholdConfigKey = "AzureMachineLearning:PositiveSentimentThreshold";

        public const string SentimentBatchRequest = "data.ashx/amla/text-analytics/v1/GetSentimentBatch";

        public const string DefaultServiceBaseUri = "https://api.datamarket.azure.com/";

        public const string ServiceBaseUriConfigKey = "AzureMachineLearning:DefaultServiceBaseUri";
    }
}