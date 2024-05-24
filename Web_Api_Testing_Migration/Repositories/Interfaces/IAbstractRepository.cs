namespace Web_Api_Testing_Migration.Repositories.Interfaces
{
    public interface IAbstractRepository<T>
    {
        ICollection<T> findAll();
        T findOneById(int id);
        T findOneBySlug(string slug);
        bool deleteOneBySlug(T model);
        bool updateBySlug(T model);
        bool deleteAll();
        bool isExistById(int id);
        bool isExistBySlug(string slug);

        bool create(T model);
        bool saveChange();
    }
}
