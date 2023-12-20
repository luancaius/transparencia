namespace Entities.DomainEntities;

public interface IEntity
{
    public string Id { get; protected set; }
}