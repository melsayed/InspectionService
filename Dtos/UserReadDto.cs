namespace InspectionService.Dtos
{
    public class UserReadDto
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? DisplayName { get; set; }
        public string? RefreshToken { get; set; } = null;
        public DateTime? RefreshTokenExpiryTime { get; set; } = null;
    }
}