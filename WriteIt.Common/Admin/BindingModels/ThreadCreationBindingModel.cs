namespace WriteIt.Common.Admin.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class ThreadCreationBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxThreadNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxThreadDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadDescriptionLength)]
        public string Description { get; set; }
    }
}