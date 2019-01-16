namespace SportsStore.Extensions
{
    using Microsoft.AspNetCore.Http;

    public static class HttpRequestExtensions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            string queryString = request.QueryString.ToString();

            return request.QueryString.HasValue ? queryString.Substring(11) : request.Path.ToString();
        }
    }
}
