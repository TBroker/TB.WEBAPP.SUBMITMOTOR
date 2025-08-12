namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs
{
    public class ApiResponseDto<T>
    {
        public string? Status { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? Timestamp { get; set; }
    }
}
