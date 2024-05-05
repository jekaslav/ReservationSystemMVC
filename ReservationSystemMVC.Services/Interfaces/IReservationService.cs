namespace ReservationSystemMVC.Services.Interfaces;

public interface IReservationService
{
    Task<bool> CreateReservation(int studentId, int classroomId, DateTimeOffset startTime, 
        DateTimeOffset endTime, CancellationToken cancellationToken);
}