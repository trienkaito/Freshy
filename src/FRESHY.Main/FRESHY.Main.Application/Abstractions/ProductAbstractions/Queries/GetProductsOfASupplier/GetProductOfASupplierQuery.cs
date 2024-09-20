using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductsOfASupplier.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductsOfASupplier;

public record GetProductOfASupplierQuery
(
    int PageNumber,
    int PageSize,
    Guid SupplierId
) : IQuery<PageQueryResult<IEnumerable<ProductsOfASupplierResult>>>;

public class GetProductOfASupplierQueryHandler : IQueryHandler<GetProductOfASupplierQuery, PageQueryResult<IEnumerable<ProductsOfASupplierResult>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;

    public GetProductOfASupplierQueryHandler(
        IProductRepository productRepository,
        IProductTypeRepository productTypeRepository)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
    }

    public async Task<PageQueryResult<IEnumerable<ProductsOfASupplierResult>>> Handle(GetProductOfASupplierQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.GetProductsBySupplierIdAsync(SupplierId.Create(request.SupplierId), product => new
            {
                product.Id,
                product.Name,
                product.FeatureImage,
                product.TypeId,
            });

            var allProducts = await _productRepository.GetAllAsync(product => new
            {
                product.Id
            });

            int totalPage = (int)Math.Ceiling((double)allProducts.Count() / request.PageSize);

            if (products is not null)
            {
                var data = products.Select(product =>
                {
                    var type = _productTypeRepository.GetByIdAsync(product!.TypeId).Result;

                    return new ProductsOfASupplierResult(
                        product!.Id.Value,
                        product.Name,
                        product.FeatureImage,
                        new ProductTypeResult(
                            type!.Id.Value,
                            type.Name
                        )
                    );
                }).ToList();

                return new PageQueryResult<IEnumerable<ProductsOfASupplierResult>>(data, request.PageNumber, request.PageSize, totalPage);
            }
            return new PageQueryResult<IEnumerable<ProductsOfASupplierResult>>(request.PageNumber, request.PageSize, HttpStatusCode.NotFound, "PRODUCTS_NOT_FOUND");
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<ProductsOfASupplierResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}