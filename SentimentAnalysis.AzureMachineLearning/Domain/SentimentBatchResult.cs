namespace SentimentAnalysis.AzureMachineLearning.Domain
{
    using System.Collections.Generic;

    internal class SentimentBatchResult
    {
        public IEnumerable<SentimentResult> SentimentBatch { get; set; }

        public IEnumerable<ErrorRecord> Errors { get; set; }
    }
}