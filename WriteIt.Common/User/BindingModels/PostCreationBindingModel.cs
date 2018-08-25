namespace WriteIt.Common.User.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class PostCreationBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxPostNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinPostNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxPostDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinPostDescriptionLength)]
        public string Description { get; set; }

        public string ThreadInstanceName { get; set; }

        public int ThreadInstanceId { get; set; }
    }
}
