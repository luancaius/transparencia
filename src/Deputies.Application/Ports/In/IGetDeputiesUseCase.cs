namespace Deputies.Application.Ports.In;

public interface IGetDeputiesUseCase
{
    Task ProcessDeputiesAsync(int year);
    Task ProcessDeputiesExpensesAsync(int year);
    Task ProcessDeputiesExpensesCurrentMonthAsync();

}