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

    /// <summary>
    /// Gets a list of all the officers.
    /// </summary>
    /// <returns>List of all the officers.</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfficerReadDto>))]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _service.GetAllAsync());

    /// <summary>
    /// Creates an officer based on the provided data.
    /// </summary>
    /// <param name="officerDto">Data of the officer to create.</param>
    /// <returns>Data of the created officer.</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(OfficerReadDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOfficerAsync(OfficerCreateDto officerDto)
    {
        var createdOfficer = await _service.CreateOfficerAsync(officerDto);
        return CreatedAtAction(nameof(GetSingleAsync), new { callSign = createdOfficer.CallSign }, createdOfficer);
    }

    /// <summary>
    /// Gets data of a single officer.
    /// </summary>
    /// <param name="callSign">Call sign of the officer to return.</param>
    /// <returns>Data of the specified officer.</returns>
    [HttpGet("{callSign}"), ActionName(nameof(GetSingleAsync))]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OfficerReadDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleAsync(string callSign)
        => Ok(await _service.GetSingleAsync(callSign));

    /// <summary>
    /// Assigns a crime event to an officer.
    /// </summary>
    /// <param name="assignOfficerDto">Data of the crime event to assign.</param>
    /// <returns>Call sign of the officer who has been assigned to the crime event.</returns>
    [HttpPost("assign")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CallSignDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignOfficer(AssignOfficerDto assignOfficerDto)
    {
        var officerCallSignDto = await _service.AssignCaseToOfficerAsync(assignOfficerDto.CrimeEventId);
        return Ok(officerCallSignDto);
    }
}
