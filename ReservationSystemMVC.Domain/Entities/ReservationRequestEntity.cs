using ReservationSystemMVC.Domain.Enums;

namespace ReservationSystemMVC.Domain.Entities;

public class ReservationRequestEntity
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ClassroomId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public ReservationStatus Status { get; set; }
    public StudentEntity Student { get; set; }
    public ClassroomEntity Classroom { get; set; }
}