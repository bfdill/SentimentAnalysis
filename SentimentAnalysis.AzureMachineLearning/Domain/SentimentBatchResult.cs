namespace SentimentAnalysis.AzureMachineLearning.Domain
{
    using System.Collections.Generic;

    public class SentimentBatchResult
    {
        public IEnumerable<SentimentResult> SentimentBatch { get; set; }

        public IEnumerable<ErrorRecord> Errors { get; set; }
    }
}