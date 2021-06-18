using System.Collections.Generic;
using UnityEngine;

namespace PolymorphismInterface
{
    public class VideoEncoder
    {
        //Using open closed principle: We can create new implementation of the interface(open) and this class remains unchanged(closed)
        private readonly IList<INotificationChannel> notificationChannels;

        public VideoEncoder()
        {
            notificationChannels = new List<INotificationChannel>();
        }

        public void Encode(DVD dvd)
        {
            Debug.Log($"Encode: {dvd.Title}");

            foreach(var channel in notificationChannels)
            {
                channel.Send(new Message(2.0f), dvd);
            }

        }

        public void RegisterNotificationChannel(INotificationChannel channel)
        {
            notificationChannels.Add(channel);
        }
    }
}

