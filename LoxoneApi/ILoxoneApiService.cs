using System.Threading.Tasks;

namespace LoxoneApi
{
    public interface ILoxoneApiService
    {
        Task SetLight(string id, int sceneId);
    }
}
