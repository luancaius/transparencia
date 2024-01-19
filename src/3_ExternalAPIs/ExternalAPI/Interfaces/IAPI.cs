namespace ExternalAPI.Interfaces;

public interface IBaseApi
{
    Task<string> GetAsync(string apiUrl);
    Task<string> PostAsync(string apiUrl, HttpContent content);
}