using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.GetAllSuppliers;

public record GetAllSuppliersQuery : IQuery<QueryResult<IEnumerable<SupplierResult>>>;

public class GetAllSuppliersQueryHandler : IQueryHandler<GetAllSuppliersQuery, QueryResult<IEnumerable<SupplierResult>>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProductRepository _productRepository;

    public GetAllSuppliersQueryHandler(
        ISupplierRepository supplierRepository,
        IProductRepository productRepository)
    {
        _supplierRepository = supplierRepository;
        _productRepository = productRepository;
    }

    public async Task<QueryResult<IEnumerable<SupplierResult>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var suppliers = await _supplierRepository.GetAllAsync();

            var data = suppliers.Select(supplier =>
            {
                var productCount = _productRepository.GetProductsBySupplierIdAsync(supplier.Id, product => new
                {
                    product.Id
                }).Result.Count();

                return new SupplierResult(
                supplier.Id.Value,
                supplier.Name,
                supplier.FeatureImage,
                supplier.Description,
                supplier.IsValid,
                (productCount > 99 ? "99+" : productCount.ToString()) ?? "0"
                );
            }).ToList();

            return new QueryResult<IEnumerable<SupplierResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<SupplierResult>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }
}