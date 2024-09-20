using FRESHY.Main.Contract.Responses.Shared;

namespace FRESHY.Main.Contract.Responses.OrderResponses;

public record OrderVoucherResponse
(
    VoucherCodeResponse VoucherCode,
    float DiscountValue
);