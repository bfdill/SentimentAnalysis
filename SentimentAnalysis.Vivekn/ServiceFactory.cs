namespace SentimentAnalysis.Vivekn
{
    using Core;
    using Core.Domain;

    public static class ServiceFactory
    {
        public static ISentimentAnalysisService Build()
        {
            var settings = new ViveknSettings();
            return new SentimentAnalysisService(new SentimentAnalysisRequestor(settings), new ErrorMessageGenerator(), settings);
        }
    }
}