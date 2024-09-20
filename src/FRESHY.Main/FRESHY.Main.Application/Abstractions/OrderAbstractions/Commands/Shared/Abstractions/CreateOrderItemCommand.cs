namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.Shared.Abstractions;

public record CreateOrderItemCommand
(
    Guid ProductId,
    Guid UnitId,
    int BoughtQuantity
);