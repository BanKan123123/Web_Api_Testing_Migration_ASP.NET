namespace Web_Api_Testing_Migration.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string eamil { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
