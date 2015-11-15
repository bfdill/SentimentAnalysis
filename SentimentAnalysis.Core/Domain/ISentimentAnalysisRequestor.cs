namespace SentimentAnalysis.Core.Domain
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface ISentimentAnalysisRequestor
    {
        Task<HttpResponseMessage> PostAsync(string request, string body);
    }
}