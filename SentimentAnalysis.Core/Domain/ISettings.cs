namespace SentimentAnalysis.Core.Domain
{
    using System;

    public interface ISettings
    {
        string GetBasicAuthenticationCredentials();

        Uri GetServiceBaseUri();

        int GetBatchLimit();

        int GetBatchByteSizeLimit();
    }
}