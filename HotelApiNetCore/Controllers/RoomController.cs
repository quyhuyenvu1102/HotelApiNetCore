using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using HotelApiNetCore.Models;
using HotelApiNetCore.Services;
using AutoMapper;

namespace HotelApiNetCore.Controllers
{
    [Route("/[controller]")]
    public class RoomController:Controller
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet(Name =nameof(GetRooms))]
        public async Task<ActionResult> GetRooms(CancellationToken ct) {
            var rooms = await _service.GetRoomsAsync(ct);  
                
            return Ok(rooms);
        }

        [HttpGet("{roomId}",Name = nameof(GetRoomById))]
        public async Task<ActionResult> GetRoomById(Guid roomId,CancellationToken ct)
        {
            var room = await _service.GetRoomByIdAsync(roomId, ct);
            if (room == null) return NotFound();

            return Ok(room);
            
        }
    }
}
