using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetAllReviewsOfAProduct.Results;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetAllReviewsOfAProduct;

public record GetAllReviewsOfAProductQuery
(
    Guid ProductId
) : IQuery<IEnumerable<AllReviewOfAProductResult>>;

public class GetAllReviewsOfAProductQueryHandler : IQueryHandler<GetAllReviewsOfAProductQuery, IEnumerable<AllReviewOfAProductResult>>
{
    public Task<IEnumerable<AllReviewOfAProductResult>> Handle(GetAllReviewsOfAProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}