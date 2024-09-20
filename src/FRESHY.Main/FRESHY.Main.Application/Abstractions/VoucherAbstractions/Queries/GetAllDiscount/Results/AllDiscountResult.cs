using FRESHY.Main.Application.Abstractions.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllDiscount.Results;

public record AllDiscountResult
(
    VoucherCodeResult VoucherCode,
    float DiscountValue
);