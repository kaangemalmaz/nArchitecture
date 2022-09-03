using Application.Features.Brands.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Brands.Models
{
    public class BrandListModal : BasePageableModel
    {
        public List<BrandListDto> Items { get; set; } //automapper map için özellikle items koyulmuş IPaginate de items diye geliyor.
    }
}
