using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Common.Domain.Common.Events;

public sealed class EventDocument
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public string Content { get; set; } = null!;
    public string? Errors { get; set; }
}