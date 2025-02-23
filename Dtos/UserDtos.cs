namespace BlogManagementSystem.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public long? UserPhoneNumber { get; set; }
        public byte [] UserDp { get; set; }
        public bool UserIsAuthor { get; set; }
        public DateTime UserCreatedAt { get; set; }
    }

    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public long UserPhoneNumber { get; set; }
        public byte [] UserDp { get; set; }
        public bool UserIsAuthor { get; set; }
    }

    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public long UserPhoneNumber { get; set; }
        public byte [] UserDp { get; set; }
        public bool UserIsAuthor { get; set; }
    }
}