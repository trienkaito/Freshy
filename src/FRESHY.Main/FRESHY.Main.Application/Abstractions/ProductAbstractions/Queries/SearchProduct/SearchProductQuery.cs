using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.SearchProduct;

public record SearchProductQuery
(
    string? ProductName,
    string? SupplierName,
    string? TypeName,
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllProductsResult>>>;

public class SearchProductQueryHandler : IQueryHandler<SearchProductQuery, PageQueryResult<IEnumerable<AllProductsResult>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ISupplierRepository _supplierRepository;

    public SearchProductQueryHandler(IProductRepository productRepository, IProductTypeRepository productTypeRepository, ISupplierRepository supplierRepository)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllProductsResult>>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allProducts = await _productRepository.GetAllAsync(product => new
            {
                product.Id,
                product.Name,
                product.FeatureImage,
                product.Description,
                product.TypeId,
                product.SupplierId,
                product.DOM,
                product.ExpiryDate,
                product.Units,
                product.IsShowToCustomer
            });

            var dataAllProductSearched = allProducts.Select(product =>
            {
                var type = _productTypeRepository.GetByIdAsync(product.TypeId).Result;

                var supplier = _supplierRepository.GetByIdAsync(product.SupplierId, supplier => new
                {
                    supplier.Id,
                    supplier.Name,
                    supplier.IsValid
                }).Result;

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
                    product.Units!.Select(unit => new ProductUnitResult(
                            unit.Id.Value,
                            unit.UnitType,
                            unit.UnitValue,
                            unit.Quantity,
                            unit.ImportPrice,
                            unit.SellPrice,
                            unit.UnitFeatureImage
                    )).ToList()
                );
            }).Where(product =>
                (string.IsNullOrEmpty(request.ProductName) || product.Name.Contains(request.ProductName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.SupplierName) || product.Supplier.Name.Contains(request.SupplierName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.TypeName) || product.Type.Name.Contains(request.TypeName, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            int totalItems = dataAllProductSearched.Count();
            int totalPage = (int)Math.Ceiling((double)totalItems / request.PageSize);

            var data = dataAllProductSearched.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            return new PageQueryResult<IEnumerable<AllProductsResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllProductsResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}