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

    /// <summary>
    /// Gets a list of all the crime events.
    /// </summary>
    /// <returns>List of all the crime events.</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CrimeEventReadDto>))]
    public async Task<IActionResult> GetAllEventsAsync()
    {
        var allEvents = await _service.GetAllCrimeEventsAsync();
        return Ok(allEvents);
    }

    /// <summary>
    /// Gets data of a single crime event.
    /// </summary>
    /// <param name="crimeEventId">Identifier of the crime event to return.</param>
    /// <returns>Data of the specified crime event.</returns>
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

    /// <summary>
    /// Creates a crime event based on the provided data.
    /// </summary>
    /// <param name="eventDto">Data of the crime event to create.</param>
    /// <returns>Data of the created crime event.</returns>
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
