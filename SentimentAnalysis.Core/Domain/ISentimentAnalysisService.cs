namespace SentimentAnalysis.Core.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISentimentAnalysisService
    {
        Task<Result> GetSentimentAsync(string text);

        Task<IDictionary<string, Result>> GetBatchSentimentAsync(Dictionary<string, string> textBatch);
    }
}