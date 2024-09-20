using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Queries.GetAllProductTypes.Resutls;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Queries.GetAllProductTypes;

public record GetAllProductTypesQuery : IQuery<QueryResult<IEnumerable<AllProductTypesResult>>>;

public class GetAllProductTypesQueryHandler : IQueryHandler<GetAllProductTypesQuery, QueryResult<IEnumerable<AllProductTypesResult>>>
{
    private readonly IProductTypeRepository _productTypeRepository;

    public GetAllProductTypesQueryHandler(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }

    public async Task<QueryResult<IEnumerable<AllProductTypesResult>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var types = await _productTypeRepository.GetAllAsync();

            var data = types.Select(type =>
            new AllProductTypesResult(
                type.Id.Value,
                type.Name
            )).ToList();

            return new QueryResult<IEnumerable<AllProductTypesResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllProductTypesResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}