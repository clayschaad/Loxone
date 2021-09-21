using System.Threading.Tasks;

namespace LoxoneApi
{
    public interface ILoxoneApiService
    {
        Task SetLight(LoxoneOptions credentials, string id, int sceneId);
        Task SetJalousie(LoxoneOptions credentials, string id, string direction);
    }
}
