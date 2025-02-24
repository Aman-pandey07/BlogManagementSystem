namespace BlogManagementSystem.Dtos.UserDtos
{
    public class GetAllUserDto
    {
        public required string UserName { get; set; }

        public required string UserEmail { get; set; }

        public long? UserPhoneNumber { get; set; }

        public string? UserDp { get; set; }

        public bool UserIsAuthor { get; set; }
    }

}