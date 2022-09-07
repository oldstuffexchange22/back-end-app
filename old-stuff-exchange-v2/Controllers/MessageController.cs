
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Model.Message;
using old_stuff_exchange_v2.Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create message")]
        public async Task<IActionResult> Create(CreateMessageModel model)
        {
            try
            {
                Message message = await _messageService.Create(model);
                if (message == null) return BadRequest();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet()]
        [SwaggerOperation(Summary = "Get list message from two user")]
        public async Task<ActionResult> GetList(Guid senderId, Guid receiverId, int pageNumber = 1, int pageSize = 30, bool isFull = false)
        {
            try
            {
                List<Message> messages = await _messageService.GetList(senderId, receiverId, pageNumber, pageSize, isFull);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = messages
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = StatusCode(StatusCodes.Status500InternalServerError),
                    exception = ex
                });
            }
        }
    }
}
