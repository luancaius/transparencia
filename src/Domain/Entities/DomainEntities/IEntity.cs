namespace Entities.DomainEntities;

public interface IEntity
{
    public Guid Id { get; protected set; }
}