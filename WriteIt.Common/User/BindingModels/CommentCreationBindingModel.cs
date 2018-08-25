namespace WriteIt.Common.User.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class CommentCreationBindingModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxCommentDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinCommentDescriptionLength)]
        public string Description { get; set; }

        public string PostName { get; set; }

        public int PostId { get; set; }
    }
}