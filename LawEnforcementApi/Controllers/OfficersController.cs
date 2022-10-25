﻿using LawEnforcementApi.DTOs;
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

    [HttpGet("{callSign}"), ActionName(nameof(GetSingleAsync))]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OfficerReadDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleAsync(string callSign)
        => Ok(await _service.GetSingleAsync(callSign));

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
