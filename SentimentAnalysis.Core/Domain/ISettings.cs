namespace SentimentAnalysis.Core.Domain
{
    using System;

    public interface ISettings
    {
        string GetApiKey();

        Uri GetServiceBaseUri();

        int GetBatchLimit();
    }
}