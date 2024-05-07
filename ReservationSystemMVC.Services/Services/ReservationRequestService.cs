using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReservationSystemMVC.Domain.Contexts;
using ReservationSystemMVC.Domain.Entities;
using ReservationSystemMVC.Domain.Enums;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Services.Services;

public class ReservationRequestService : IReservationRequestService
{
        private ReservationSystemDbContext ReservationSystemDbContext { get; }
        
        private IMapper Mapper { get; }

        public ReservationRequestService(ReservationSystemDbContext context, IMapper mapper)
        {
            ReservationSystemDbContext = context;
            Mapper = mapper;
        }
        
        public async Task<IEnumerable<ReservationRequestDto>> GetAllRequests(CancellationToken cancellationToken)
        {
            var requests = await ReservationSystemDbContext.ReservationRequests
                .AsNoTracking()
                .Select(x => Mapper.Map<ReservationRequestDto>(x))
                .ToListAsync(cancellationToken);
            
            if (requests is null)
            {
                throw new NullReferenceException();
            }
            
            return requests;
        }

        public async Task<ReservationRequestDto> GetRequestById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new BadHttpRequestException("Invalid ID");
            }
            
            var request = await ReservationSystemDbContext.ReservationRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (request is null)
            {
                throw new ArgumentException($"Reservation request with ID {id} does not exist.");
            }

            var result = Mapper.Map<ReservationRequestDto>(request);

            return result;
        }
        
        public async Task<bool> Create(ReservationRequestDto requestDto, CancellationToken cancellationToken)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }
            
            var startTime = requestDto.StartTime.UtcDateTime;
            var endTime = requestDto.EndTime.UtcDateTime;

            var newRequest = new ReservationRequestEntity()
            {
                Id = requestDto.Id,
                ClassroomId = requestDto.ClassroomId,
                StudentId = requestDto.StudentId,
                StartTime = startTime,
                EndTime = endTime,
            };
    
            ReservationSystemDbContext.ReservationRequests.Add(newRequest);
            await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        
        public async Task<bool> Update(int id, ReservationRequestDto requestDto, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new BadHttpRequestException("Invalid reservation request ID");
            }

            var requestToUpdate = await ReservationSystemDbContext.ReservationRequests
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            requestToUpdate.ClassroomId = requestDto.ClassroomId;
            requestToUpdate.StudentId = requestDto.StudentId;
            requestToUpdate.StartTime = requestDto.StartTime;
            requestToUpdate.EndTime = requestDto.EndTime;

            await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        
        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new BadHttpRequestException("Invalid reservation request ID");
            }
            
            var requestToDelete = await ReservationSystemDbContext.ReservationRequests
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            ReservationSystemDbContext.ReservationRequests.Remove(requestToDelete);
            
            await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
        
        public async Task<List<ReservationRequestEntity>> GetRequestsForClassroom(int classroomId, int chiefId, CancellationToken cancellationToken)
        {
            var chiefClassroom = await ReservationSystemDbContext.ChiefClassrooms
                .Where(x => x.ClassroomId == classroomId)
                .Where(x => x.ChiefId == chiefId)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (chiefClassroom is null)
            {
                return null;
            }
            
            var requests = await ReservationSystemDbContext.ReservationRequests
                .Where(x => x.ClassroomId == classroomId)
                .ToListAsync(cancellationToken);
            
            return requests;
        }

        public async Task<bool> UpdateReservationRequestStatus(int reservationRequestId, int chiefId, ReservationStatus newStatus, CancellationToken cancellationToken)
        {
            var request = await ReservationSystemDbContext.ReservationRequests
                .FirstOrDefaultAsync(x => x.Id == reservationRequestId, cancellationToken);
            if (request is null)
            {
                return false;
            }

            var chiefClassroom = await ReservationSystemDbContext.ChiefClassrooms
                .Where(x => x.ClassroomId == request.ClassroomId)
                .Where(x => x.ChiefId == chiefId)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (chiefClassroom is null)
            {
                return false;
            }

            var reservationService = new ReservationService(ReservationSystemDbContext);
            var success = await reservationService.CreateReservation(request.StudentId, request.ClassroomId,
                request.StartTime, request.EndTime, cancellationToken);
            
            if (!success)
            {
                return false;
            }

            request.Status = newStatus;

            await using var transaction = await ReservationSystemDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var reservationCreated = await reservationService.CreateReservation(request.StudentId, request.ClassroomId, 
                    request.StartTime, request.EndTime, cancellationToken);
                
                if (!reservationCreated)
                {
                    throw new Exception("reservation does not exist");
                }
  
                request.Status = newStatus;
                await ReservationSystemDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }
        }
}