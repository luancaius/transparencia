namespace ExternalAPI.Interfaces;

public interface IAPI
{
    Task<string> GetAsync(string apiUrl);
    Task<string> PostAsync(string apiUrl, HttpContent content);
}