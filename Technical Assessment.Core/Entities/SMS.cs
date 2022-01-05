namespace Technical_Assessment.Core.Entities
{
    public class SMS
    {
        public int ID { get; set; }
        public string PhoneNumber { get; set; } = "default phonenumber";
        public string Message { get; set; } = "default message";
    }
}
