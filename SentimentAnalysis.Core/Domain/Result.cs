namespace SentimentAnalysis.Core.Domain
{
    public abstract class Result
    {
        protected Result(bool success, string error, Sentiment sentiment)
        {
            Success = success;
            Error = error;
            Sentiment = sentiment;
        }

        public bool Success { get; }

        public string Error { get; }

        public Sentiment Sentiment { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var r = (Result)obj;

            return Success == r.Success && Sentiment == r.Sentiment && Error == r.Error;
        }

        public override int GetHashCode() => Sentiment.GetHashCode() ^ Success.GetHashCode() ^ Error?.GetHashCode() ?? 0;
    }
}