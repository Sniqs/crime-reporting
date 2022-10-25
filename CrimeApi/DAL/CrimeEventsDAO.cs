using CrimeApi.DAL.Interfaces;
using CrimeApi.Entities;
using CrimeApi.Exceptions;
using MongoDB.Driver;

namespace CrimeApi.DAL;

public class CrimeEventsDAO : ICrimeEventsDAO
{
    private readonly IConfiguration _configuration;
    private readonly IMongoCollection<CrimeEvent> _crimeEventsCollection;

    public CrimeEventsDAO(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration["CrimeEventsDB:ConnectionString"];
        var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
        var mongoClient = new MongoClient(settings);
        var mongoDatabase = mongoClient.GetDatabase(_configuration["CrimeEventsDB:DatabaseName"]);
        _crimeEventsCollection = mongoDatabase.GetCollection<CrimeEvent>(_configuration["CrimeEventsDB:CrimeEventsCollection"]);
    }
    public async Task CreateCrimeEventAsync(CrimeEvent crimeEvent)
        => await _crimeEventsCollection.InsertOneAsync(crimeEvent);


    public async Task<IEnumerable<CrimeEvent>> GetAllAsync()
        => await _crimeEventsCollection.Find(_ => true).ToListAsync();

    public async Task<CrimeEvent> GetSingleAsync(string id)
    {
        var crimeEvent = await _crimeEventsCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        if (crimeEvent is null)
            throw new ResourceNotFoundException($"Crime event with id {id} doesn't exist.");
        return crimeEvent;
    }

    public async Task UpdateCrimeEventAsync(CrimeEvent crimeEvent)
    {
        var update = Builders<CrimeEvent>.Update.Set(nameof(CrimeEvent.AssignedOfficerCallSign), crimeEvent.AssignedOfficerCallSign);
        await _crimeEventsCollection.UpdateOneAsync(e => e.Id == crimeEvent.Id, update);
    }
}
