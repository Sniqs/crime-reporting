using LawEnforcementApi.DTOs;
using LawEnforcementApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace LawEnforcementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfficersController : ControllerBase
{
	private readonly IOfficersService _service;

	public OfficersController(IOfficersService service)
	{
		_service = service;
	}

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfficerReadDto>))]
    public async Task<IActionResult> GetAllAsync()
		=> Ok(await _service.GetAllAsync());

    [HttpGet("assign/{crimeEventId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public async Task<IActionResult> AssignOfficer(string crimeEventId)
    {
        var officerCallSignDto = await _service.AssignCaseToOfficerAsync(crimeEventId);
        return Ok(officerCallSignDto);
    }



    [HttpGet("{callSign}")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OfficerReadDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleAsync(string callSign)
        => Ok(await _service.GetSingleAsync(callSign));
}
