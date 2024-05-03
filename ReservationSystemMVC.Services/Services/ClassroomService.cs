using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReservationSystemMVC.Domain.Contexts;
using ReservationSystemMVC.Domain.Entities;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Services.Services;

public class ClassroomService : IClassroomService
{
    private ReservationSystemDbContext ReservationSystemDbContext { get; }
    private IMapper Mapper { get; }
    
    public ClassroomService(ReservationSystemDbContext context, IMapper mapper)
    {
        ReservationSystemDbContext = context;
        Mapper = mapper;
    }

    public async Task<IEnumerable<ClassroomDto>> GetAllClassrooms(CancellationToken cancellationToken)
    {
        var classrooms = await ReservationSystemDbContext.Classrooms
            .AsNoTracking()
            .Select(x => Mapper.Map<ClassroomDto>(x))
            .ToListAsync(cancellationToken);
        
        if (classrooms is null)
        {
            throw new NullReferenceException();
        }
        return classrooms;
    }
    
    public async Task<ClassroomDto> GetClassroomById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
            
        var classroom = await ReservationSystemDbContext.Classrooms
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (classroom is null)
        {
            throw new NullReferenceException();
        }
            
        var result = Mapper.Map<ClassroomDto>(classroom);

        return result;
    }
    
    public async Task<bool> Create(ClassroomDto classroomDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(classroomDto.Location))
        {
            throw new ArgumentException();
        }
            
        var newClassroom = new ClassroomEntity()
        {
            Id = classroomDto.Id,
            RoomNumber = classroomDto.RoomNumber,
            Capacity = classroomDto.Capacity,
            Location = classroomDto.Location
        };
            
        ReservationSystemDbContext.Classrooms.Add(newClassroom);
            
        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);
            
        return true;
    }
    
    public async Task<bool> Update(int id, ClassroomDto classroomDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
        
        var classroomToUpdate = await ReservationSystemDbContext.Classrooms
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (classroomToUpdate is null)
        {
            throw new NullReferenceException();
        }
            
        classroomToUpdate.RoomNumber = classroomDto.RoomNumber ?? classroomToUpdate.RoomNumber;
        classroomToUpdate.Capacity = classroomDto.Capacity ?? classroomToUpdate.Capacity;
        classroomToUpdate.Location = classroomDto.Location ?? classroomToUpdate.Location;

        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
            
        var classroomToDelete = await ReservationSystemDbContext.Classrooms
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (classroomToDelete is null)
        {
            throw new NullReferenceException();
        }

        ReservationSystemDbContext.Classrooms.Remove(classroomToDelete);
            
        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);
            
        return true;
    }
}