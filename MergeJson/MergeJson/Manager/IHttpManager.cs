using System.Threading.Tasks;

namespace MergeJson.Manager
{
    public interface IHttpManager
    {
        Task<string> GetMergedJson();
    }
}