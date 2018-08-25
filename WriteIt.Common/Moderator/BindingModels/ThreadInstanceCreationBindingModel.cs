namespace WriteIt.Common.Moderator.BindingModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class ThreadInstanceCreationBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxThreadInstanceNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadInstanceNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxThreadInstanceDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = WebConstants.ThreadName)]
        public string ThreadId { get; set; }

        public string ThreadName { get; set; }

        public string CreatorId { get; set; }

        public DateTime DateOfRegistry { get; set; }

        public IEnumerable<SelectListItem> Threads { get; set; }
    }
}