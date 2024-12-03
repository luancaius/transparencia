namespace Deputies.Application.Ports.In;

public interface IGetDeputiesUseCase
{
    Task<IEnumerable<DeputyResponse>> GetDeputiesAsync(int year);
}

public record DeputyResponse(
    string Id,
    string Name,
    string Party,
    string State);