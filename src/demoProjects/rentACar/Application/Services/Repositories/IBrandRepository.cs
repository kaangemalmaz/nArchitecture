using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    //Sadece brande özel operasyonlar gelecek!
    public interface IBrandRepository : IAsyncRepository<Brand>, IRepository<Brand>
    {

    }
}
