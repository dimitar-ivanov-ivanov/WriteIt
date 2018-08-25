namespace WriteIt.Models
{
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxCommentDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinCommentDescriptionLength)]
        public string Description { get; set; }

        public int Karma { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}