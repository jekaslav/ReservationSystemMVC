using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllStudents(CancellationToken cancellationToken);
    Task<StudentDto> GetStudentById(int id, CancellationToken cancellationToken);
    Task<bool> Create(StudentDto studentDto, CancellationToken cancellationToken);
    Task<bool> Update(int id, StudentDto studentDto, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}