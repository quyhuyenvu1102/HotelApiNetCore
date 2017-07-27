using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelApiNetCore.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace HotelApiNetCore.Services
{
    public class DefaultRoomService:IRoomService
    {
        public readonly HotelApiDbContext _context;

        public DefaultRoomService(HotelApiDbContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomByIdAsync(Guid Id,CancellationToken ct)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == Id,ct);

            var room = Mapper.Map<Room>(entity);
            return room;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken ct)
        {
            var query = _context.Rooms.ProjectTo<Room>();

            return await query.ToArrayAsync();
        }
    }
}