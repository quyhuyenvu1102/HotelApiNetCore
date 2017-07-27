using HotelApiNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApiNetCore
{
    public class HotelApiDbContext:DbContext
    {
        public HotelApiDbContext(DbContextOptions option) : base(option) { }

        public DbSet<RoomEntity> Rooms { get; set; }

    }
}
