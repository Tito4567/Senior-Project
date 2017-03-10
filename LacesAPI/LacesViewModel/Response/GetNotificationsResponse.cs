using System;
using System.Collections.Generic;

namespace LacesViewModel.Response
{
    public class GetNotificationsResponse : LacesResponse
    {
        public List<Notification> Notifications { get; set; }
    }

    public class Notification
    {
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NotificationType { get; set; }
    }
}
