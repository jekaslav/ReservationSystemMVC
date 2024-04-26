namespace ReservationSystemMVC.Web.Models;

public class ClassroomDto
{
    public int Id { get; set; }
    public int? RoomNumber { get; set; }
    public int? Capacity { get; set; }
    public string? Location { get; set; }
}