namespace WriteIt.Common.Moderator.ViewModels
{
    using System;

    public class ThreadInstanceDetailsViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateOfRegistry { get; set; }

        public string Creator { get; set; }

        public int PostsCount { get; set; }

        public int TotalKarma { get; set; }
    }
}