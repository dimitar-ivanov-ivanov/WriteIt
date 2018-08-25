namespace WriteIt.Common.Admin.ViewModels
{
    using System.Collections.Generic;

    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
