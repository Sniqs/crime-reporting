using CrimeApi.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrimeApi.Entities;

public class CrimeEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTime OccurrenceTime { get; set; }

    public EventType Type { get; set; }

    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string ReporterEmail { get; set; } = null!;
    public Status EventStatus { get; set; }
    public string AssignedOfficer { get; set; } = null!;
}
