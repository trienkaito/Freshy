using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts;

public record GetAllProductsQuery
(
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllProductsResult>>>;

public class GetAllProductQueryHandler : IQueryHandler<GetAllProductsQuery, PageQueryResult<IEnumerable<AllProductsResult>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public GetAllProductQueryHandler(
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

    public async Task<PageQueryResult<IEnumerable<AllProductsResult>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.GetByPagingAsync(request.PageNumber, request.PageSize, product => new
            {
                product.Id,
                product.Name,
                product.FeatureImage,
                product.Description,
                product.TypeId,
                product.SupplierId,
                product.DOM,
                product.ExpiryDate,
                product.IsShowToCustomer
            });

            var allProducts = await _productRepository.GetAllAsync(product => new
            {
                product.Id
            });

            int totalPage = (int)Math.Ceiling((double)allProducts.Count() / request.PageSize);

            var data = products.Select(product =>
            {
                var type = _productTypeRepository.GetByIdAsync(product.TypeId).Result;

                var supplier = _supplierRepository.GetByIdAsync(product.SupplierId, supplier => new
                {
                    supplier.Id,
                    supplier.Name,
                    supplier.IsValid
                }).Result;

                var units = _productUnitRepository.GetProductUnitsByProductId(product.Id).Result;

                return new AllProductsResult
                (
                    product.Id.Value,
                    product.Name,
                    product.FeatureImage,
                    product.Description,
                    new ProductTypeResult(
                        product.TypeId.Value,
                        type!.Name
                    ),
                    new ProductSupplierResult(
                        product.SupplierId.Value,
                        supplier!.Name,
                        supplier.IsValid
                    ),
                    product.DOM,
                    product.ExpiryDate,
                    product.IsShowToCustomer,
                    units?.Select(unit => new ProductUnitResult(
                        unit.Id.Value,
                        unit.UnitType,
                        unit.UnitValue,
                        unit.Quantity,
                        unit.ImportPrice,
                        unit.SellPrice,
                        unit.UnitFeatureImage
                    )).ToList()
                );
            });
            return new PageQueryResult<IEnumerable<AllProductsResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllProductsResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}