using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<BrandListModal>
    {
        public PageRequest PageRequest { get; set; } //kaçıncı sayfa ve kaçtane istiyorsun diye requesti yolluyor client.

        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModal>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;

            public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }
            public async Task<BrandListModal> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                BrandListModal mappedBrandListModal = _mapper.Map<BrandListModal>(brands);

                return mappedBrandListModal;
            }
        }
    }
}
