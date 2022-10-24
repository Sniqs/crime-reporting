﻿using CrimeApi.DAL.Interfaces;
using CrimeApi.Entities;
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
    
}