﻿using HotelApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApiNetCore.Controllers
{
    [Route("/[controller]")]
    public class InfoController : Controller
    {
        private readonly HotelInfo _hotelInfo;

        public InfoController(IOptions<HotelInfo> hotelInfoAccessor) {
            _hotelInfo = hotelInfoAccessor.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        public IActionResult GetInfo() {
            return Ok(_hotelInfo);
        }

    }
}
