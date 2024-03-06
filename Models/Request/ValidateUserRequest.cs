namespace SimpleEntry.Models.Request
{
    public class ValidateUserRequest
    {
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
