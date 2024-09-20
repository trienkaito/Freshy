using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.ImportProduct;

public record ImportProductCommand
(
    string Name,
    string FeatureImage,
    string Description,
    Guid TypeId,
    Guid SupplierId,
    string Dom,
    string ExpiryDate,
    bool IsShowToCustomer,
    Guid EmployeeId,
    List<CreateProductUnitCommand>? Units
) : ICommand<CommandResult>;

public record CreateProductUnitCommand
(
    double ImportPrice,
    int Quantity,
    double SellPrice,
    string UnitFeatureImage,
    string UnitType,
    double UnitValue
);

public class ImportedProductCommandHandler : ICommandHandler<ImportProductCommand, CommandResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public ImportedProductCommandHandler(
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository)
    {
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
    }

    public async Task<CommandResult> Handle(ImportProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _productRepository.UnitOfWork.BeginTransaction();

            var product = Product.Create(
                request.Name,
                request.FeatureImage,
                request.Description,
                ProductTypeId.Create(request.TypeId),
                SupplierId.Create(request.SupplierId),
                Convert.ToDateTime(request.Dom),
                Convert.ToDateTime(request.ExpiryDate),
                request.IsShowToCustomer
            );
            var unitDictionary = new Dictionary<Guid, int>();
            if (request.Units is not null)
            {
                var units = new List<ProductUnit>();
                foreach (var unit in request.Units)
                {
                    var addedUnit = ProductUnit.Create(
                        product.Id,
                        unit.UnitType,
                        unit.UnitValue,
                        unit.Quantity,
                        unit.ImportPrice,
                        unit.SellPrice,
                        unit.UnitFeatureImage
                    );

                    units.Add(addedUnit);
                    unitDictionary.Add(addedUnit.Id.Value, addedUnit.Quantity);
                }

                await _productUnitRepository.InsertRange(units);
            }

            var @event = new ProductBeingImported(
                product.Id.Value,
                request.EmployeeId,
                unitDictionary
            );

            product.AddDomainEvent(@event);

            await _productRepository.InsertAsync(product);
            await _productRepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}