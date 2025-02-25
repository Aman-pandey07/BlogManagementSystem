using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }

        public required string BlogTitle { get; set; }

        public required string BlogContent { get; set; }

        public string? BlogImage { get; set; }

        public required DateTime CreatedAt { get; set; }


        public int UserId { get; set; }  //FK to User table

        //public int UserName { get; set; }  //FK to User table//we dont need this as we can retrive user name once we get the id 

        public required UserModel UserModel { get; set; } //Navigatin property to represent manager


        public int CategoryId { get; set; } //FK to category

        public required CategoryModel CategoryModel { get; set; } //Navigatin property to category

        public ICollection<CommentModel> CommentModels { get; set; } = new List<CommentModel>(); //one to many with comments
    }
}
