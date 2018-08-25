namespace WriteIt.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WriteIt.Utilities.Constants;

    public class User : IdentityUser
    {
        public User()
        {
            this.Comments = new List<Comment>();
            this.Posts = new List<Post>();
            this.CreatedInstances = new List<ThreadInstance>();
            this.SubscribedToThreads = new List<UserThreadInstance>();
        }

        [Required]
        [Display(Name = WebConstants.FullName)]
        [StringLength(ValidationConstants.MaxUserFullNameLength, ErrorMessage = ErrorMessages.InvalidLength, MinimumLength = ValidationConstants.MinUserFullNameLength)]
        public string FullName { get; set; }

        public int Karma { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfRegistry { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<ThreadInstance> CreatedInstances { get; set; }

        public ICollection<UserThreadInstance> SubscribedToThreads { get; set; }
    }
}