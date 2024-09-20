using FRESHY.Main.Application.Abstractions.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;

public record OrderVoucherResult
(
    VoucherCodeResult VoucherCode,
    float DiscountValue
);