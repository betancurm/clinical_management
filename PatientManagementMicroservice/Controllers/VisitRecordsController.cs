using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagementMicroservice.DTOs;
using PatientManagementMicroservice.Services;

namespace PatientManagementMicroservice.Controllers;

[ApiController]
[Route("api/patients/{numeroIdentificacion}/visits")]
//[Authorize(Roles = "Enfermera")]
public class VisitRecordsController : ControllerBase
{
    private readonly IVisitRecordService _visitRecordService;

    public VisitRecordsController(IVisitRecordService visitRecordService)
    {
        _visitRecordService = visitRecordService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVisitRecord(string numeroIdentificacion, [FromBody] CreateVisitRecordDto dto)
    {
        var visitRecord = await _visitRecordService.CreateAsync(numeroIdentificacion, dto);
        return CreatedAtAction(nameof(GetVisitRecord), new { numeroIdentificacion, visitId = visitRecord.Id }, visitRecord);
    }

    [HttpPut("{visitId}")]
    public async Task<IActionResult> UpdateVisitRecord(string numeroIdentificacion, Guid visitId, [FromBody] UpdateVisitRecordDto dto)
    {
        var updatedVisitRecord = await _visitRecordService.UpdateAsync(numeroIdentificacion, visitId, dto);
        return Ok(updatedVisitRecord);
    }

    [HttpGet]
    //[Authorize(Roles = "Enfermera,PersonalAdministrativo,Medico")]
    public async Task<IActionResult> GetVisitRecords(string numeroIdentificacion)
    {
        var visitRecords = await _visitRecordService.GetByPatientAsync(numeroIdentificacion);
        return Ok(visitRecords);
    }

    [HttpGet("{visitId}")]
    //[Authorize(Roles = "Enfermera,PersonalAdministrativo,Medico")]
    public async Task<IActionResult> GetVisitRecord(string numeroIdentificacion, Guid visitId)
    {
        var visitRecord = await _visitRecordService.GetByIdAsync(numeroIdentificacion, visitId);
        return Ok(visitRecord);
    }
}