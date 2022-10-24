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

    [BsonRepresentation(BsonType.String)]
    public EventType Type { get; set; }

    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string ReporterEmail { get; set; } = null!;
    
    [BsonRepresentation(BsonType.String)]
    public Status EventStatus { get; set; }
    public string AssignedOfficerCallSign { get; set; } = null!;
}
