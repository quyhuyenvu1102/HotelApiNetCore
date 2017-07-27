using System;
using System.Threading;
using System.Threading.Tasks;
using HotelApiNetCore.Models;
using System.Collections.Generic;

namespace HotelApiNetCore.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomByIdAsync(Guid id,CancellationToken ct);

        Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken ct);
    }
}