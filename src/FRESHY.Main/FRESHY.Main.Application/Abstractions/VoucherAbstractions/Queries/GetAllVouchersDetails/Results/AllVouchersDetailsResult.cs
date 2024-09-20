using FRESHY.Main.Application.Abstractions.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllVouchersDetails.Results;

public record AllVouchersDetailsResult
(
    VoucherCodeResult VoucherCode,
    DateTime StartedOn,
    DateTime EndedOn,
    float DiscountValue,
    string? Description
);