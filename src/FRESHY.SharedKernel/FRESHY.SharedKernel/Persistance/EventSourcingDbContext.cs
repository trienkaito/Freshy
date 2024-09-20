using FRESHY.Common.Domain.Common.Events;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FRESHY.SharedKernel.Persistance;

public class EventSourcingDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public EventSourcingDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FRESHY_EVENT_DB");
        var databaseName = "FRESHY_EVENT_DB";

        var mongoClient = new MongoClient(connectionString);
        _mongoDatabase = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<EventDocument> Events => _mongoDatabase.GetCollection<EventDocument>("Events");
}