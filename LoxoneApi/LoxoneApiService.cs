using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoxoneApi
{
    public class LoxoneApiService : ILoxoneApiService
    {
        public async Task SetLight(LoxoneOptions options, string id, int sceneId)
        {
            var requestUrl = $"http://{options.Host}/dev/sps/io/{id}/{sceneId}";
            var result = await DoGet(options, requestUrl);
        }

        public async Task SetJalousie(LoxoneOptions options, string id, string direction)
        {
            var requestUrl = $"http://{options.Host}/dev/sps/io/{id}/{direction}";
            var result = await DoGet(options, requestUrl);
        }

        private async Task<HttpResponseMessage> DoGet(LoxoneOptions options, string requestUrl)
        {
            using var client = new HttpClient();
            var authToken = Encoding.ASCII.GetBytes($"{options.Username}:{options.Password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            return await client.GetAsync(requestUrl);
        }
    }
}
