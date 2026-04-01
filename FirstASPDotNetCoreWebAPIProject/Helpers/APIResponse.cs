namespace FirstASPDotNetCoreWebAPIProject.Helpers
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static ApiResponse Ok (string? message,object? data) 
            => new ApiResponse { Success = true, Message = message, Data = data };

        public static ApiResponse Fail (string? message)
            => new ApiResponse { Success = false, Message = message};
    }
}
