namespace LanguageFeatures.Models
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class MyAsyncMethods
    {
        public static async Task<long?> GetPathLengthAsync()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage httpMessage = await client.GetAsync("http://apress.com");

            return httpMessage.Content.Headers.ContentLength;
        }
    }
}
