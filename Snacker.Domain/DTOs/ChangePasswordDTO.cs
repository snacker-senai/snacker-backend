namespace Snacker.Domain.DTOs
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
