using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.UpdateProduct;

public record UpdateProductCommand
(
    Guid Id,
    string Name,
    string FeatureImage,
    string Description,
    Guid TypeId,
    Guid SupplierId,
    string Dom,
    string ExpiryDate,
    bool IsShowToCustomer,
    Guid EmployeeId
) : ICommand<CommandResult>;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, CommandResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productRepository.UnitOfWork.BeginTransaction();

            var product = await _productRepository.GetByIdAsync(ProductId.Create(request.Id));
            var products = await _productRepository.GetAllAsync();

            if (product is null)
                return new CommandResult(HttpStatusCode.BadRequest, null);

            var isNameChanged = !request.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase);
            var isNameDuplicated = products.Any(p => p.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase) && p.Id != product.Id);

            if (isNameChanged && isNameDuplicated)
                return new CommandResult(HttpStatusCode.BadRequest, "Name Duplicated !!!");

            product.UpdateProductInfos(
                request.Name,
                request.FeatureImage,
                request.Description,
                ProductTypeId.Create(request.TypeId),
                SupplierId.Create(request.SupplierId),
                Convert.ToDateTime(request.Dom),
                Convert.ToDateTime(request.ExpiryDate),
                request.IsShowToCustomer
            );

            var @event = new ProductBeingUpdated(
                product!.Id.Value,
                request.EmployeeId
            );

            product.AddDomainEvent(@event);
            await _productRepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}