using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoxoneApi
{
    public class LoxoneApiService : ILoxoneApiService
    {
        private const string Host = "";
        private const string Username = "";
        private const string Password = "";

        public async Task SetLight(string id, int sceneId)
        {
            var requestUrl = $"http://{Host}/dev/sps/io/{id}/{sceneId}";
            var result = await DoGet(requestUrl);
        }

        public async Task SetJalousie(string id, string direction)
        {
            var requestUrl = $"http://{Host}/dev/sps/io/{id}/{direction}";
            var result = await DoGet(requestUrl);
        }

        private async Task<HttpResponseMessage> DoGet(string requestUrl)
        {
            using var client = new HttpClient();
            var authToken = Encoding.ASCII.GetBytes($"{Username}:{Password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            return await client.GetAsync(requestUrl);
        }
    }
}
