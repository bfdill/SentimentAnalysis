namespace SentimentAnalysis.Vivekn.Domain
{
    using System;

    public interface IViveknSettings
    {
        int GetBatchByteSizeLimit();

        Uri GetServiceBaseUri();
    }
}