using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Old_stuff_exchange.Controllers;
using old_stuff_exchange_v2.Model;
using old_stuff_exchange_v2.Service;
using System;
using System.Collections.Generic;
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

        [HttpPost("/send2")]
        public async Task<ActionResult> SendNotification2()
        {

            // This registration token comes from the client FCM SDKs.
            var registrationToken = "e9neiBicPBABUOw4DJ_p7A:APA91bF-TQ7SksoqyVK4oNy8zf7EX-IyW1ixCij0ZSJaVA8TNzpNsrBwa6b19VzhMgx9XA-AOj7JT-OnrIkZp55bzh2AquCGR7SJsBSdPjz-Ic9_sRw-M_3STN8j8UrbLJTzFZtvSoP5";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
    {
        { "myData", "1337" },
    },
                Token = registrationToken,
                Notification = new Notification()
                {
                    Title = "Test from code",
                    Body = "Here is your test!"
                }
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            string response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
            // Response is a message ID string.
            Console.WriteLine("Successfully sent message: " + response);
            return Ok(response);
        }
    }
}
