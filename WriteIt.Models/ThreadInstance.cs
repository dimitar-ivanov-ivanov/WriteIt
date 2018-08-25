namespace WriteIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class ThreadInstance
    {
        public ThreadInstance()
        {
            this.Posts = new List<Post>();
            this.Subscribers = new List<UserThreadInstance>();
        }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.MaxThreadInstanceNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadInstanceNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxThreadInstanceDescriptionLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinThreadInstanceDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfRegistry { get; set; }

        public int ThreadId { get; set; }

        public Thread Thread { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<UserThreadInstance> Subscribers { get; set; }
    }
}
