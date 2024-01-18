using System.Linq.Expressions;
using Entities.DomainEntities;

namespace Services.Interfaces;

public interface IPersonService
{
    Task RefreshPersonTableFromMongo();
}