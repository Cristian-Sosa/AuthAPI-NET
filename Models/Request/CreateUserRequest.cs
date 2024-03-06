namespace SimpleEntry.Models.Request
{
    public class CreateUserRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
    }
}
