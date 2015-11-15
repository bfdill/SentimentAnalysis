using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.Core.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITextAnalyticsService
    {
        Task<Result> GetSentimentAsync(string text);

        Task<IDictionary<string, Result>> GetBatchSentimentAsync(Dictionary<string, string> textBatch);
    }
}
