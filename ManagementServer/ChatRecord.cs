namespace ManagementServer
{
    public class ChatRecord
    {
        public string Sender { get; set; } = "";
        public string TargetAgent { get; set; } = "";
        public string Message { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
