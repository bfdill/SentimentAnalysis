namespace SentimentAnalysis.Core
{
    public static class Constants
    {
        public const string AuthorizationHeaderName = "Authorization";

        public const string BasicAuthenticationCredentialsConfigKey = "BasicAuthenticationCredentials";

        public const string ServiceBaseUriConfigKey = "ServiceBaseUri";

        public const string BatchLimitConfigKey = "BatchLimit";

        public const int DefaultBatchLimit = 1000;

        public const string BatchByteSizeLimitConfigKey = "BatchByteSizeLimit";

        public const int DefaultBatchByteSizeLimit = 900000;

        public const string SentimentNullInputErrorText = "Unable to extract a sentiment from null or empty text";

        public const string ErrorWithOkResultErrorText = "Cannot construct a SentimentResult with an error and a status code of HttpStatusCode.OK";

        public const string BatchLimitExceededErrorText = "The number of requests is greated than the batch limit.";

        public const string ScoreOutOfRangeError = "Score must be a number between 0 and 1";

        public const string SentimentRequest = "data.ashx/amla/text-analytics/v1/GetSentiment?Text=";

        public const string SentimentBatchRequest = "data.ashx/amla/text-analytics/v1/GetSentimentBatch";
    }
}