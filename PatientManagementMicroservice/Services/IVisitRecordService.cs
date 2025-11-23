using PatientManagementMicroservice.DTOs;

namespace PatientManagementMicroservice.Services;

public interface IVisitRecordService
{
    Task<VisitRecordDto> CreateAsync(string numeroIdentificacion, CreateVisitRecordDto dto);
    Task<VisitRecordDto> UpdateAsync(string numeroIdentificacion, Guid visitId, UpdateVisitRecordDto dto);
    Task<IEnumerable<VisitRecordDto>> GetByPatientAsync(string numeroIdentificacion);
    Task<VisitRecordDto> GetByIdAsync(string numeroIdentificacion, Guid visitId);
}