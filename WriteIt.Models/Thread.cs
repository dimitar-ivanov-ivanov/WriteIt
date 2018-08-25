namespace WriteIt.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class Thread
    {
        public Thread()
        {
            this.Instances = new List<ThreadInstance>();
        }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxThreadNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxThreadDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadDescriptionLength)]
        public string Description { get; set; }

        public ICollection<ThreadInstance> Instances { get; set; }
    }
}
