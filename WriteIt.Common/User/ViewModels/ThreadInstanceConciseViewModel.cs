namespace WriteIt.Common.User.ViewModels
{
    public class ThreadInstanceConciseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DateOfRegistry { get; set; }

        public string CreatorName { get; set; }

        public int PostsCount { get; set; }

        public int TotalKarma { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public bool IsUserSubscribed { get; set; }

        public string UserId { get; set; }
    }
}