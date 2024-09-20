using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.CreateReview;
using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.ReplyReview;
using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetAllReviewsOfAProduct.Results;
using FRESHY.Main.Application.Abstractions.ReviewAbstractions.Queries.GetReviewDetails.Results;
using FRESHY.Main.Contract.Requests.ReviewRequests;
using FRESHY.Main.Contract.Responses.ReviewResponses;
using Mapster;

namespace FRESHY_Service.Config.Mapster;

public class ReviewMappingConfiguraion : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Requests Mapping
        config.NewConfig<CreateReviewRequest, CreateReviewCommand>();
        config.NewConfig<ReplyReviewRequest, ReplyReviewCommand>();

        //Responses Mapping
        config.NewConfig<AllReviewOfAProductResult, AllReviewsOfAProductResponse>();
        config.NewConfig<ReviewDetailsResult, ReviewDetailsResponse>();
    }
}