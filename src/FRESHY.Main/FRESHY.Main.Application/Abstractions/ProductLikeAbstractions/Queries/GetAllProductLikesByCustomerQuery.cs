using System.Net;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Queries.Resutls;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;

namespace FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Queries;

public record GetAllProductLikesByCustomerQuery
(
    Guid CustomerId
) : IQuery<QueryResult<IEnumerable<AllProductLikesByCustomerResult>>>;

public class GetAllProductLikesByCustomerQueryHandler : IQueryHandler<GetAllProductLikesByCustomerQuery, QueryResult<IEnumerable<AllProductLikesByCustomerResult>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductLikerepository _productLikerepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public GetAllProductLikesByCustomerQueryHandler(
        IProductRepository productRepository,
        IProductLikerepository productLikerepository,
        IProductTypeRepository productTypeRepository,
        IProductUnitRepository productUnitRepository)
    {
        _productRepository = productRepository;
        _productLikerepository = productLikerepository;
        _productTypeRepository = productTypeRepository;
        _productUnitRepository = productUnitRepository;
    }
    public async Task<QueryResult<IEnumerable<AllProductLikesByCustomerResult>>> Handle(GetAllProductLikesByCustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var productLikes = await _productLikerepository.GetProductLikesByCustomerId(CustomerId.Create(request.CustomerId));

            if (productLikes is null)
            {
                return new QueryResult<IEnumerable<AllProductLikesByCustomerResult>>(HttpStatusCode.NotFound, "NOT_FOUND");
            }

            var data = productLikes.Select(like =>
            {
                var product = _productRepository.GetByIdAsync(like.ProductId, product => new
                {
                    product.Id,
                    product.FeatureImage,
                    product.Name,
                    product.TypeId
                }).Result;

                if (product is null)
                {
                    return null;
                }
                var type = _productTypeRepository.GetByIdAsync(product.TypeId).Result;

                var units = _productUnitRepository.GetProductUnitsByProductId(product.Id).Result;

                var lowestPrice = units!.Min(unit => unit.SellPrice);
                return new AllProductLikesByCustomerResult(
                    product.Id.Value,
                    product.Name,
                    product.FeatureImage,
                    product.TypeId.Value,
                    type!.Name,
                    lowestPrice,
                    like.Id.Value
                );
            }).ToList();

            return new QueryResult<IEnumerable<AllProductLikesByCustomerResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllProductLikesByCustomerResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
