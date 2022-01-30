using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services.Interface;

namespace ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IRoomService _roomService;

        public HotelController(ILogger<HotelController> logger, IRoomService roomService)
        {
            this._roomService = roomService;
            this._logger = logger;
        }

        [HttpPost]
        [Route("GetAvailableRooms")]
        public AppResponse<IList<ComboItem>> GetAvailableRooms([FromBody] AppRequest<string> request)
        {
            AppResponse<IList<ComboItem>> response = new AppResponse<IList<ComboItem>>();
            response.Content = _roomService.GetAvailableRooms();
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("AssignRoom")]
        public AppResponse<string> AssignRoom([FromBody] AppRequest<string> request)
        {
            AppResponse<string> response = new AppResponse<string>();
            response.Content = _roomService.AssignRoom();
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("CheckoutRoom")]
        public AppResponse<bool> CheckoutRoom([FromBody] AppRequest<string> request)
        {
            AppResponse<bool> response = new AppResponse<bool>();
            response.Content = _roomService.CheckoutRoom(request.Content);
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("GetOccupiedRooms")]
        public AppResponse<IList<ComboItem>> GetOccupiedRooms([FromBody] AppRequest<string> request)
        {
            AppResponse<IList<ComboItem>> response = new AppResponse<IList<ComboItem>>();
            response.Content = _roomService.GetOccupiedRooms();
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("GetVacantRooms")]
        public AppResponse<IList<ComboItem>> GetVacantRooms([FromBody] AppRequest<string> request)
        {
            AppResponse<IList<ComboItem>> response = new AppResponse<IList<ComboItem>>();
            response.Content = _roomService.GetVacantRooms();
            response.Success = true;
            return response;
        }


        [HttpPost]
        [Route("MarkRoomCleaned")]
        public AppResponse<bool> MarkRoomCleaned([FromBody] AppRequest<string> request)
        {
            AppResponse<bool> response = new AppResponse<bool>();
            response.Content = _roomService.MarkRoomCleaned(request.Content);
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("MarkRoomForRepair")]
        public AppResponse<bool> MarkRoomForRepair([FromBody] AppRequest<string> request)
        {
            AppResponse<bool> response = new AppResponse<bool>();
            response.Content = _roomService.MarkRoomForRepair(request.Content);
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("MarkRepairCompleted")]
        public AppResponse<bool> MarkRepairCompleted([FromBody] AppRequest<string> request)
        {
            AppResponse<bool> response = new AppResponse<bool>();
            response.Content = _roomService.MarkRepairCompleted(request.Content);
            response.Success = true;
            return response;
        }

        [HttpPost]
        [Route("GetRepairingRooms")]
        public AppResponse<IList<ComboItem>> GetRepairingRooms([FromBody] AppRequest<string> request)
        {
            AppResponse<IList<ComboItem>> response = new AppResponse<IList<ComboItem>>();
            response.Content = _roomService.GetRepairingRooms();
            response.Success = true;
            return response;
        }
        //GetRepairingRooms
        [HttpPost]
        [Route("RestAllRoomStatuses")]
        public AppResponse<bool> RestAllRoomStatuses([FromBody] AppRequest<string> request)
        {
            AppResponse<bool> response = new AppResponse<bool>();
            response.Content = _roomService.ResetAllRoomStatuses();
            response.Success = true;
            return response;
        }

    }
}
