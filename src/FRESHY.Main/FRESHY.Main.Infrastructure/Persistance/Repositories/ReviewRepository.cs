using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class ReviewRepository : Repository<Review, ReviewId, FreshyDbContext>, IReviewRepository
{
    public ReviewRepository(FreshyDbContext context) : base(context)
    {
    }
}