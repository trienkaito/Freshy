namespace FRESHY.Main.Contract.Requests.VoucherRequests;

public record DeActiveVouchersRequest
(
    Guid EmployeeId,
    List<Guid> VoucherIds
);