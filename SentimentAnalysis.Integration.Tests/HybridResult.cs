namespace SentimentAnalysis.Integration.Tests
{
    using Core.Domain;

    public class HybridResult
    {
        public decimal Confidence { get; set; }

        public string Error { get; set; }

        public string Id { get; set; }

        public decimal Score { get; set; }

        public Sentiment Sentiment { get; set; }

        public string Text { get; set; }
    }
}