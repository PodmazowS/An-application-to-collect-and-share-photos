namespace Infrastructure.MongoDB
{
    public class MongoDBSettings
    {
        public string ConnectionUri { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PhotoCollection { get; set; } = null!;
        public string AlbumCollection { get; set; } = null!;
        public string UserCollection { get; set; } = null!;
        public string UserRoleCollection { get; set; } = null!;
        public string LikeCollection { get; set; } = null!;
        public string CommentCollection { get; set; } = null!;
    }
}
