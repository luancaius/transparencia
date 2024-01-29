using System.Linq.Expressions;

namespace Services.Interfaces;

public interface IPersonService
{
    Task RefreshPersonTableFromMongo();
}