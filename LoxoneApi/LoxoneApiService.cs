using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoxoneApi
{
    public class LoxoneApiService : ILoxoneApiService
    {
        private const string Host = "192.168.1.xx";
        private const string Username = "xxx";
        private const string Password = "xxx";

        public async Task SetLight(string id, int sceneId)
        {
            var requestUrl = $"http://{Host}/dev/sps/io/{id}/{sceneId}";
            using var client = new HttpClient();
            var authToken = Encoding.ASCII.GetBytes($"{Username}:{Password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            var result = await client.GetAsync(requestUrl);
        }
    }
}
