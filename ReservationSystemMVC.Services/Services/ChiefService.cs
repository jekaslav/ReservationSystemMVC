using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ReservationSystemMVC.Domain.Contexts;
using ReservationSystemMVC.Domain.Entities;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Services.Services;

public class ChiefService : IChiefService
{
    private ReservationSystemDbContext ReservationSystemDbContext { get; }
    private IMapper Mapper { get; }

    public ChiefService(ReservationSystemDbContext context, IMapper mapper)
    {
        ReservationSystemDbContext = context;
        Mapper = mapper;
    }
    
    public async Task<IEnumerable<ChiefDto>> GetAllChiefs(CancellationToken cancellationToken)
    {
        var chiefs = await ReservationSystemDbContext.Chiefs
            .AsNoTracking()
            .Select(x => Mapper.Map<ChiefDto>(x))
            .ToListAsync(cancellationToken);
        
        return chiefs;
    }
    
    public async Task<ChiefDto> GetChiefById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
            
        var chiefs = await ReservationSystemDbContext.Chiefs
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => Mapper.Map<ChiefDto>(x))
                .FirstOrDefaultAsync(cancellationToken);

        var result = Mapper.Map<ChiefDto>(chiefs);

        return result;
    }
    
    public async Task<bool> Create(ChiefDto chiefDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(chiefDto.Name))
        {
            throw new ArgumentException();
        }
            
        if (string.IsNullOrWhiteSpace(chiefDto.Email))
        {
            throw new ArgumentException();
        }
            
        var newChief = new ChiefEntity()
        {
            Id = chiefDto.Id,
            Name = chiefDto.Name,
            Email = chiefDto.Email
        };
            
        ReservationSystemDbContext.Chiefs.Add(newChief);

        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Update(int id, ChiefDto chiefDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
            
        var chiefToUpdate = await ReservationSystemDbContext.Chiefs
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (chiefToUpdate is null)
        {
            throw new NullReferenceException();
        }

        chiefToUpdate.Name = chiefDto.Name ?? chiefToUpdate.Name;
        chiefToUpdate.Email = chiefDto.Email ?? chiefDto.Email;

        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
            
        var chiefToDelete = await ReservationSystemDbContext.Chiefs
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (chiefToDelete is null)
        {
            throw new NullReferenceException();
        }

        ReservationSystemDbContext.Chiefs.Remove(chiefToDelete);

        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    
    public async Task<bool> TakeControl(int classroomId, int chiefId, CancellationToken cancellationToken)
    {
        if (classroomId <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
        if (chiefId <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }

        var chiefClassroom = new ChiefClassroomEntity
        {
            ChiefId = chiefId,
            ClassroomId = classroomId
        };

        ReservationSystemDbContext.ChiefClassrooms.Add(chiefClassroom);
        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
        
    public async Task<bool> ReleaseControl(int classroomId, int chiefId, CancellationToken cancellationToken)
    {
        if (classroomId <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
        if (chiefId <= 0)
        {
            throw new BadHttpRequestException("Invalid ID");
        }
            
        var chiefClassroom = await ReservationSystemDbContext.ChiefClassrooms
            .FirstOrDefaultAsync(x => x.ClassroomId == classroomId && x.ChiefId == chiefId, cancellationToken);
            
        if (chiefClassroom is null)
        {
            return false;
        }
            
        ReservationSystemDbContext.ChiefClassrooms.Remove(chiefClassroom);
        await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);
            
        return true;
    }
}