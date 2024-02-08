public interface IRelationalDatabase
{
    void SaveEntity<T>(T entity);
}
