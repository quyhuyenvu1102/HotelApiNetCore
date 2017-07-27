using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApiNetCore.Controllers
{
    [Route("/")]
    public class RootController:Controller
    {
        [HttpGet(Name =nameof(GetRoot))]
        public IActionResult GetRoot() {
            var response = new {
                Href = Url.Link(nameof(GetRoot), null),
                Info = Url.Link(nameof(InfoController.GetInfo), null),
                Rooms = Url.Link(nameof(RoomController.GetRooms), null),
                Rel = "self"
            };

            return Ok(response);
        }


    }
}
