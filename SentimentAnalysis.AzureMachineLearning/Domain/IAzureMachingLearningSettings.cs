namespace SentimentAnalysis.AzureMachineLearning.Domain
{
    using System;

    public interface IAzureMachingLearningSettings
    {
        string GetBasicAuthenticationCredentials();

        int GetBatchItemLimit();

        decimal GetNegativeSentimentThreshold();

        decimal GetPositiveSentimentThreshold();

        Uri GetServiceBaseUri();
    }
}