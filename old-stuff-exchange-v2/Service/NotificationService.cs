using CorePush.Google;
using Microsoft.Extensions.Options;
using old_stuff_exchange_v2.Configs;
using old_stuff_exchange_v2.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Service
{
    public interface INotificationService
    {
        Task<string> SendNotification(NotificationModel notificationModel);
    }
    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }
        public async Task<string> SendNotification(NotificationModel notificationModel)
        {
            string response = "";
            try
            {
                FcmSettings settings = new FcmSettings()
                {
                    ServerKey = _fcmNotificationSetting.ServerKey,
                    SenderId = _fcmNotificationSetting.SenderId
                };
                HttpClient httpClient = new HttpClient();
                string authorizationKey = string.Format("key={0}", settings.ServerKey);
                string deviceToken = notificationModel.DeviceId;
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                GoogleNotification.DataPayload dataPayload = new GoogleNotification.DataPayload();
                dataPayload.Title = notificationModel.Title;
                dataPayload.Body = notificationModel.Body;

                GoogleNotification notification = new GoogleNotification();
                notification.Data = dataPayload;
                notification.Notification = dataPayload;

                var fcm = new FcmSender(settings, httpClient);
                var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);
                if (fcmSendResponse.IsSuccess())
                {
                    response = "Notification sent successfully";
                    return response;
                }
                else
                {
                    response = fcmSendResponse.Results[0].Error;
                    return response;
                }

            }
            catch (Exception)
            {
                response = "Something went wrong";
                return response;
            }
        }
    }
}
