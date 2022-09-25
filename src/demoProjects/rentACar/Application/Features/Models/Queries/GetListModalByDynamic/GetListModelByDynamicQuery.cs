using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModalByDynamic
{
    public class GetListModelByDynamicQuery : IRequest<ModelListModel>
    {
        public Dynamic Dynamic { get; set; } //bu dinamik sorgu yazmak istediğini söylediğin yer.
        public PageRequest PageRequest { get; set; }

        public class GetListModalByDynamicQueryHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
        {

            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListModalByDynamicQueryHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
            {
                //car models
                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(
                                                            dynamic: request.Dynamic,
                                                            include:
                                                                m => m.Include(c => c.Brand),
                                                                index: request.PageRequest.Page,
                                                                size: request.PageRequest.PageSize
                                                                );

                //data Model
                ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}



//{
//    "sort": [
//      {
//        "field": "name",
//      "dir": "asc"
//      }
//  ],
//  "filter": {
//        "field": "name",
//    "operator": "eq",
//    "value": "Series 4",
//    "logic": "or",
//    "filters": [
//      {
//            "field": "dailyPrice",
//      "operator": "gte",
//      "value": "1000"
//      }
//    ]
//  }
//}