using CrimeApi.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CrimeApi.DTOs;

public record CrimeEventReadDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public DateTime OccurrenceTime { get; init; }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EventType Type { get; init; }

    public string Description { get; init; } = null!;
    public string Location { get; init; } = null!;
    public string ReporterEmail { get; init; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status EventStatus { get; init; }
    public string AssignedOfficerCallSign { get; init; } = null!;
}
