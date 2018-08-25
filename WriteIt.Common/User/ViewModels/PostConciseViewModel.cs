namespace WriteIt.Common.User.ViewModels
{
    public class PostConciseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Karma { get; set; }

        public int ThreadInstanceId { get; set; }

        public string CreatorName { get; set; }

        public int CommentsCount { get; set; }
    }
}