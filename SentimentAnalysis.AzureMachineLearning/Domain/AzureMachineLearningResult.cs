namespace SentimentAnalysis.AzureMachineLearning.Domain
{
    using Core.Domain;

    public class AzureMachineLearningResult : Result
    {
        private AzureMachineLearningResult(decimal score, bool success, string error, Sentiment sentiment)
            : base(success, error, sentiment)
        {
            Score = score;
        }

        public decimal Score { get; }

        public static AzureMachineLearningResult Build(decimal score, Sentiment sentiment)
        {
            return new AzureMachineLearningResult(score, true, null, sentiment);
        }

        public static AzureMachineLearningResult Build(string error)
        {
            return new AzureMachineLearningResult(decimal.Zero, false, error, Sentiment.Invalid);
        }
    }
}