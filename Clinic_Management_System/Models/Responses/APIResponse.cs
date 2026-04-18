namespace Clinic_Management_System.Models.Responses
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int TotalRecorde { get; set; }
    }
}
