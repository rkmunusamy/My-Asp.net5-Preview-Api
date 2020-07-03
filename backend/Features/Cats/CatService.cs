using backend.Data;
using backend.Models.Models;
using System.Threading.Tasks;

namespace backend.Features.Cats
{
    public class CatService : ICatService
    {
        private readonly BackendDbContext data;

        public CatService(BackendDbContext data) => this.data = data;

        public async Task<int> Create(string imageUrl, string description, string userId)
        {
            var cat = new Cat
            {
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.data.Add(cat);

            await this.data.SaveChangesAsync();

            return cat.Id;
        }
    }
}
