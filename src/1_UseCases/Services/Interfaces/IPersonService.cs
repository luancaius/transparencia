namespace Services.Interfaces;

public interface IPersonService
{
    Task RefreshPersonTableFromMongo();
}