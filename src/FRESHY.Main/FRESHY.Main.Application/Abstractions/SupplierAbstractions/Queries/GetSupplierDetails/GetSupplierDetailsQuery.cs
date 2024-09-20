using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.GetSupplierDetails;

public record GetSupplierDetailsQuery
(
    Guid SupplierId
) : IQuery<QueryResult<SupplierResult>>;

public class GetSupplierDetailsQueryHandler : IQueryHandler<GetSupplierDetailsQuery, QueryResult<SupplierResult>>
{
    private readonly ISupplierRepository _supplierRepository;

    public GetSupplierDetailsQueryHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<QueryResult<SupplierResult>> Handle(GetSupplierDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var supplierDetails = await _supplierRepository.GetByIdAsync(SupplierId.Create(request.SupplierId), supplier => new
            {
                supplier.Id,
                supplier.Name,
                supplier.FeatureImage,
                supplier.Description,
                supplier.IsValid,
                supplier.Products
            });
            var productCount = supplierDetails!.Products.Count;

            var data = new SupplierResult(
            supplierDetails.Id.Value,
            supplierDetails.Name,
            supplierDetails.FeatureImage,
            supplierDetails.Description,
            supplierDetails.IsValid,
            (productCount > 99 ? "99+" : productCount.ToString()) ?? "0"
            );

            return new QueryResult<SupplierResult>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<SupplierResult>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}