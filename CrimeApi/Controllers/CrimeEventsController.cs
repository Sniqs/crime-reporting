using CrimeApi.DTOs;
using CrimeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace CrimeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CrimeEventsController : ControllerBase
{
    private readonly ICrimeEventsService _service;
    private readonly ILogger<CrimeEventsController> _logger;

    public CrimeEventsController(ICrimeEventsService service, ILogger<CrimeEventsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CrimeEventReadDto>))]
    public async Task<IActionResult> GetAllEventsAsync()
    {
        var allEvents = await _service.GetAllCrimeEventsAsync();
        return Ok(allEvents);
    }

    [HttpGet("{crimeEventId}"), ActionName(nameof(GetSingleEventAsync))]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CrimeEventReadDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleEventAsync(string crimeEventId)
    {
        if (ObjectId.TryParse(crimeEventId, out _))
            return Ok(await _service.GetSingleCrimeEventAsync(crimeEventId));

        _logger.LogInformation("Incorrect event id: {eventId}.", crimeEventId);
        return BadRequest(new { Message = "Incorrect event id." });
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(CrimeEventReadDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEventAsync(CrimeEventCreateDto eventDto)
    {
        var createdEvent = await _service.CreateCrimeEventAsync(eventDto);
        return CreatedAtAction(nameof(GetSingleEventAsync), new { crimeEventId = createdEvent.Id }, createdEvent);
    }
}
