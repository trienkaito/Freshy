using FRESHY.Main.Contract.Responses.Shared;

namespace FRESHY.Main.Contract.Responses.VoucherResponses;

public record AllVoucherDetailsResponse
(
    VoucherCodeResponse VoucherCode,
    DateTime StartedOn,
    DateTime EndedOn,
    float DiscountValue,
    string? Description
);