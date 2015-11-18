namespace SentimentAnalysis.Vivekn.Domain
{
    using Core.Domain;

    public class ViveknResult : Result
    {
        private ViveknResult(decimal confidence, bool success, string error, Sentiment sentiment)
            : base(success, error, sentiment)
        {
            Confidence = confidence;
        }

        public decimal Confidence { get; }

        public static ViveknResult Build(decimal confidence, Sentiment sentiment)
        {
            return new ViveknResult(confidence, true, null, sentiment);
        }

        public static ViveknResult Build(string error)
        {
            return new ViveknResult(decimal.Zero, false, error, Sentiment.Invalid);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Confidence.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && ((ViveknResult)obj).Confidence == Confidence;
        }
    }
}