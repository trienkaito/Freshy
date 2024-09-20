using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductDetails.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductDetails;

public record GetProductDetailsQuery
(
    Guid ProductId
) : IQuery<QueryResult<ProductDetailsResult>>;

public class GetProductDetailsQueryHandler : IQueryHandler<GetProductDetailsQuery, QueryResult<ProductDetailsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public GetProductDetailsQueryHandler(
        IProductRepository productRepository,
        IProductTypeRepository productTypeRepository,
        ISupplierRepository supplierRepository,
        IProductUnitRepository productUnitRepository)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _supplierRepository = supplierRepository;
        _productUnitRepository = productUnitRepository;
    }

    public async Task<QueryResult<ProductDetailsResult>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId), product => new
            {
                product.Id,
                product.Name,
                product.FeatureImage,
                product.Description,
                product.CreatedDate,
                product.UpdatedDate,
                product.DOM,
                product.ExpiryDate,
                product.SupplierId,
                product.TypeId,
                product.IsShowToCustomer
            });

            if (product is not null)
            {
                var productType = await _productTypeRepository.GetByIdAsync(product.TypeId);

                var productSupplier = await _supplierRepository.GetByIdAsync(product!.SupplierId, supplier => new
                {
                    supplier.Id,
                    supplier.Name,
                    supplier.IsValid
                });

                var units = _productUnitRepository.GetProductUnitsByProductId(product.Id).Result;

                var data = new ProductDetailsResult(
                    product!.Id.Value,
                    product.Name,
                    product.FeatureImage,
                    product.Description,
                    new ProductTypeResult(
                        productType!.Id.Value,
                        productType.Name
                    ),
                    new ProductSupplierResult(
                        product.SupplierId.Value,
                        productSupplier!.Name,
                        productSupplier.IsValid
                    ),
                    product.CreatedDate,
                    product.UpdatedDate,
                    product.DOM,
                    product.ExpiryDate,
                    product.IsShowToCustomer,
                    units?.Select(unit =>
                    new ProductUnitResult(
                        unit.Id.Value,
                        unit.UnitType,
                        unit.UnitValue,
                        unit.Quantity,
                        unit.ImportPrice,
                        unit.SellPrice,
                        unit.UnitFeatureImage
                    )).ToList()
                );
                return new QueryResult<ProductDetailsResult>(data);
            }
            return new QueryResult<ProductDetailsResult>(HttpStatusCode.NotFound, "PRODUCT_NOT_FOUND");
        }
        catch (Exception e)
        {
            return new QueryResult<ProductDetailsResult>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}