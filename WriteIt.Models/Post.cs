namespace WriteIt.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class Post
    {
        public Post()
        {
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxPostNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinPostNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxPostDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinPostDescriptionLength)]
        public string Description { get; set; }

        public int Karma { get; set; }

        public int ThreadInstanceId { get; set; }

        public ThreadInstance ThreadInstance { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}