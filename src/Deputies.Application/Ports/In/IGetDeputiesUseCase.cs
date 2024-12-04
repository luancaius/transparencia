namespace Deputies.Application.Ports.In;

public interface IGetDeputiesUseCase
{
    Task ProcessDeputiesAsync(int year);
}