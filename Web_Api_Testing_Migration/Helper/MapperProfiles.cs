using AutoMapper;
using Web_Api_Testing_Migration.Dto;
using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Helper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //Mapper book
            CreateMap<book, BookDto>()
            .ForMember(dest => dest.categories, opt => opt.MapFrom(src => src.categories.Select(c => c.category)))
            .ForMember(dest => dest.author, opt => opt.MapFrom(src => src.author));

            CreateMap<BookDto, book>()
                .ForMember(dest => dest.categories, opt => opt.MapFrom(src => src.categories.Select(c => new category { id = c.id, name = c.name, slug = c.slug, created_at = c.created_at, updated_at = c.updated_at})))
                .ForMember(dest => dest.author, opt => opt.MapFrom(src => src.author));

            // Mapper categories on book
            CreateMap<categoriesonbook, CategoryDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.category.id))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.category.name));
            CreateMap<CategoryDto, categoriesonbook>();

            CreateMap<category, categoriesonbook>();
            CreateMap<categoriesonbook, category>();

            //Mapper author
            CreateMap<author, AuthorDto>();
            CreateMap<AuthorDto, author>();

            //Mapper category
            CreateMap<category, CategoryDto>();
            CreateMap<CategoryDto, category>();


            //Mapper chapter
            CreateMap<chapter, ChapterDto>()
                .ForMember(dest => dest.book, opt => opt.MapFrom(src => src.book));
            CreateMap<ChapterDto, chapter>().ForMember(dest => dest.book, opt => opt.MapFrom(src => src.book));
            
            //Mapepr bookChapter
            CreateMap<BookChapter, BookChapterDto>()
                .ForMember(dest => dest.chapters, opt => opt.MapFrom(src => src.chapters));
            CreateMap<BookChapterDto, BookChapter>()
               .ForMember(dest => dest.chapters, opt => opt.MapFrom(src => src.chapters));

            //Mapper users
            CreateMap<users, UserDto>();
            CreateMap<UserDto, users>();
            CreateMap<users, UserLoginDto>();
            CreateMap<UserLoginDto, users>();
        }
    }
}
