namespace WriteIt.Common.Moderator.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class ThreadInstanceEditBindingModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxThreadInstanceNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadInstanceNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxThreadInstanceDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadDescriptionLength)]
        public string Description { get; set; }

        public string ThreadName { get; set; }
    }
}