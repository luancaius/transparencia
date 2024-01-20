namespace DeputyUseCase.Interfaces;

public interface IDeputyUseCase
{
    Task GetDeputies(int legislatura);
    Task GetDeputiesExpenses(int year);
}