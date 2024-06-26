namespace ReservationSystemMVC.Domain.Entities;

public class ClassroomEntity
{
    public int Id { get; set; }
    public int? RoomNumber { get; set; }
    public int? Capacity { get; set; }
    public string Location { get; set; }
    public IEnumerable<ReservationRequestEntity> ReservationRequestList { get; set; }
    public IEnumerable<ReservationEntity> ReservationList { get; set; }
    public IEnumerable<ChiefClassroomEntity> ChiefClassrooms { get; set; }
}