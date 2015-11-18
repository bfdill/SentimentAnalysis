namespace SentimentAnalysis.Core
{
    public static class Constants
    {
        public const string SentimentNullInputErrorText = "Unable to extract a sentiment from null or empty text";

        public const string ErrorWithOkResultErrorText = "Cannot construct a SentimentResult with an error and a status code of HttpStatusCode.OK";

        public const string ScoreOutOfRangeError = "Score must be a number between 0 and 1";
    }
}