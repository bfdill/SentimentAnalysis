namespace SentimentAnalysis.Vivekn
{
    public class Constants
    {
        public const string BatchByteSizeLimitExceededErrorText = "The size of the batch is greater than the batch byte size limit.";

        public const string BatchByteSizeLimitConfigKey = "Vivekn:BatchByteSizeLimit";

        public const int DefaultBatchByteSizeLimit = 900000;

        public const string SentimentBatchRequest = "batch/";

        public const string DefaultServiceBaseUri = "http://sentiment.vivekn.com/api/";

        public const string ServiceBaseUriConfigKey = "Vivekn:DefaultServiceBaseUri";
    }
}