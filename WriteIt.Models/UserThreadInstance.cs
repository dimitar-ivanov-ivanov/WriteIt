namespace WriteIt.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserThreadInstance
    {
        public string SubscriberId { get; set; }

        public User Subscriber { get; set; }

        public int ThreadInstanceId { get; set; }

        public ThreadInstance ThreadInstance { get; set; }
    }
}