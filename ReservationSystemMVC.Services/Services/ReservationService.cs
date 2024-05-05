using Microsoft.EntityFrameworkCore;
using ReservationSystemMVC.Domain.Contexts;
using ReservationSystemMVC.Domain.Entities;
using ReservationSystemMVC.Services.Interfaces;

namespace ReservationSystemMVC.Services.Services;

public class ReservationService : IReservationService
{
    private readonly ReservationSystemDbContext ReservationSystemDbContext;

    public ReservationService(ReservationSystemDbContext dbContext)
    {
        ReservationSystemDbContext = dbContext;
    }

    public async Task<bool> CreateReservation(int studentId, int classroomId, DateTimeOffset startTime,
        DateTimeOffset endTime, CancellationToken cancellationToken)
    {
        var existingReservation = ReservationSystemDbContext.Reservations
            .Where(x => x.ClassroomId == classroomId)
            .Where(x => x.StartTime < endTime)
            .Where(x => x.EndTime > startTime)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingReservation is not null)
        {
            return false;
        }

        var reservation = new ReservationEntity
        {
            StudentId = studentId,
            ClassroomId = classroomId,
            StartTime = startTime,
            EndTime = endTime
        };

        ReservationSystemDbContext.Reservations.Add(reservation);

        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}