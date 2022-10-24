using CrimeApi.DTOs;
using CrimeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrimeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CrimeEventsController : ControllerBase
{
	private readonly ICrimeEventsService _service;

	public CrimeEventsController(ICrimeEventsService service)
	{
		_service = service;
	}

    [HttpGet]
    public async Task<IActionResult> GetAllEventsAsync()
    {
        var allEvents = await _service.GetAllCrimeEventsAsync();
        return Ok(allEvents);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventAsync(CrimeEventCreateDto eventDto)
    {
        var createdEvent = await _service.CreateCrimeEventAsync(eventDto);
        return Ok(createdEvent);
    }
}
