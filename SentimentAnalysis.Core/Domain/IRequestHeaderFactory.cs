namespace SentimentAnalysis.Core.Domain
{
    using System.Net.Http.Headers;

    public interface IRequestHeaderFactory
    {
        string AuthorizationHeader();

        MediaTypeWithQualityHeaderValue AcceptHeader();
    }
}