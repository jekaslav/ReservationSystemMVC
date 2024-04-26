namespace ReservationSystemMVC.Domain.Entities;

public class ChiefEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<ChiefClassroomEntity> ChiefClassrooms { get; set; }
}