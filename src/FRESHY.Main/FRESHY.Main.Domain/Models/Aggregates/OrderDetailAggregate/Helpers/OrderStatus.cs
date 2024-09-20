namespace FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;

public enum OrderStatus
{
    SUCCESSED,
    CREATED, //The initial status when an order is created but not yet processed.
    PROCESSING, //Payment has been confirmed, and the order is being processed.
    DELIVERED, //The order has been successfully delivered to the customer.
    CANCELED, //The customer or the system has canceled the order.
    RETURNED //The customer has returned part or all of the order.
}