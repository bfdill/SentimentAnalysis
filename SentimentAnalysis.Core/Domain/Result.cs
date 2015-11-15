namespace SentimentAnalysis.Core.Domain
{
    public class Result
    {
        private Result(bool success, string error, Sentiment sentiment)
        {
            Success = success;
            Error = error;
            Sentiment = sentiment;
        }

        public bool Success { get; }

        public string Error { get; }

        public Sentiment Sentiment { get; }

        public static Result Build(Sentiment sentiment)
        {
            return new Result(true, null, sentiment);
        }

        public static Result Build(string error)
        {
            return new Result(false, error, Sentiment.Invalid);
        }

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