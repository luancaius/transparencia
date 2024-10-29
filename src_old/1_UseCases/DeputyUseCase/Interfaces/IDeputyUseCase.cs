namespace DeputyUseCase.Interfaces;

public interface IDeputyUseCase
{
    Task GetAndStoreDeputiesDetailsInfo(int year);
    Task GetAndStoreDeputiesExpenses(int year);
}