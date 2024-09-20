using FRESHY.Main.Contract.Requests.Shared;

namespace FRESHY.Main.Contract.Requests.VoucherRequests;

public record CreateVoucherRequest
(
    VoucherCodeRequest? Code,
    string StartedOn,
    string EndedOn,
    float DiscountValue,
    string? Description,
    Guid EmployeeId
);