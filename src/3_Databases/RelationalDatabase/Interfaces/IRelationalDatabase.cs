namespace RelationalDatabase.Interfaces;

public interface IRelationalDatabase
{
    void SaveEntity<T>(T entity) where T:class;
}