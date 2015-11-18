namespace SentimentAnalysis.AzureMachineLearning
{
    using Core;
    using Core.Domain;

    public static class ServiceFactory
    {
        public static ISentimentAnalysisService Build()
        {
            var settings = new AzureMachineLearningSettings();
            return new SentimentAnalysisService(new SentimentAnalysisRequestor(new RequestHeaderFactory(settings), settings), new ErrorMessageGenerator(), settings);
        }
    }
}