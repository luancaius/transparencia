namespace ExternalAPI.Interfaces;

public interface IApi
{
    Task<string> GetAsync(string apiUrl);
    Task<string> PostAsync(string apiUrl, HttpContent content);
}