using Web_Api_Testing_Migration.Data;
using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration
{
    public class Seed
    {
        private readonly DataContext context;

        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void SeedDataContext()
        {

            if (!context.author.Any())
            {
                var authors = new List<author>()
                {
                    new author()
                    {
                        name = "Tu Xuong",
                        slug = "tu-xuong",
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                    },
                    new author()
                    {
                        name = "Name Cao",
                        slug = "nam-cao",
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                    }
                };

                context.author.AddRange(authors);
            }

            if (!context.category.Any())
            {
                var categories = new List<category>()
                {
                    new category()
                    {
                        name = "Chuyen Ngan",
                        slug = "chuyen-ngan",
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                    },
                    new category()
                    {
                        name = "Chuyen Ngu Ngon",
                        slug = "chuyen-ngu-ngon",
                        created_at = DateTime.Now,
                        updated_at= DateTime.Now,
                    }
                };

                context.category.AddRange(categories);
            }

            if (!context.book.Any())
            {
                var books = new List<book>()
                {
                    new book()
                    {
                        title = "Thuong Vo",
                        slug = "thuong-vo",
                        description = "Thương vợ dựng lên hai bức chân dung: bức chân dung hiện thực của bà Tú và bức chân dung tinh thần của Tú Xương. Trong những bài thơ viết về vợ của Tú Xương, dường như bao giờ người ta cũng gặp hai hình ảnh song hành: bà Tú hiện lên phía trước và ông Tú khuất lấp ở phía sau.",
                        imageThumbnail  = "/images/thuong-vo",
                        rate = 4,
                        view = 12000,
                        realView = null,
                        like = 5214,
                        disable = 0,
                        hiddenAds = 0,
                        status = "FULL",
                        author = new author()
                        {
                            name = "Tu Xuong",
                            slug = "tu-xuong",
                            created_at = DateTime.Now,
                            updated_at = DateTime.Now
                        },
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                    }
                };
                context.book.AddRange(books);
            }
            context.SaveChanges();
        }
    }
}
