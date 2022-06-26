using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using old_stuff_exchange_v2.Model;
using old_stuff_exchange_v2.Service;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Controllers
{
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<ActionResult> SendNotification(NotificationModel notificationModel)
        {
            return Ok(await _notificationService.SendNotification(notificationModel));
        }
    }
}
