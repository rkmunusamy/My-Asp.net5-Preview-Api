using System.Threading.Tasks;

namespace backend.Features.Cats
{
    public interface ICatService
    {
        public Task<int> Create(string imageUrl, string description, string userId);

    }
}
