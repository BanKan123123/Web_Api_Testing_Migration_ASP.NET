﻿namespace Web_Api_Testing_Migration.Dto
{
    public class CategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
