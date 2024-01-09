using Entities.DomainEntities;
using Entities.ValueObject;
using NonRelationalDatabase.Interfaces;
using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;
using RelationalDatabase.Interfaces;
using Repositories.DTO.NewApi.Expense;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.NewApi.GetById;
using Repositories.DTO.OldApi.GetAll;
using Repositories.DTO.OldApi.GetById;
using Repositories.Interfaces;
using Serilog;
using Services.DTO;
using Services.DTO.Deputy;
using Services.Interfaces;
using Services.Mapper;
using DeputyExpense = Repositories.DTO.NewApi.Expense.DeputyExpense;

namespace Services.Service;

public class DeputyService : IDeputyService
{
    private readonly INonRelationalDatabase _nonRelationalDatabase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISearchDeputyRepository _searchDeputyRepository;
    private readonly ILogger _logger;
    private const double THRESHOLD_VALUE = 1000;
    
    public DeputyService(ISearchDeputyRepository searchDeputyRepository, ILogger logger,
        INonRelationalDatabase nonRelationalDatabase, IUnitOfWork unitOfWork)
    {
        _searchDeputyRepository = searchDeputyRepository;
        _logger = logger.ForContext<DeputyService>();
        _nonRelationalDatabase = nonRelationalDatabase;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeputiesListDto> GetDeputiesListExternalApi(int legislatura)
    {
        _logger.Information($"GetDeputiesListExternalApi {legislatura}");
        var deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);

        var deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);

        var deputiesList = new DeputiesListDto(deputiesListOldApi, deputiesListNewApi);
        return deputiesList;
    }

    public async Task<DeputiesDetailListDto> GetDeputiesDetailListExternalApi(int legislatura)
    {
        var deputiesDetailListNewApi = new List<DeputyDetailNewApi>();
        DeputiesListNewApi deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);
        foreach (var deputy in deputiesListNewApi.DeputiesNewApi)
        {
            var id = Convert.ToInt32(deputy.Id);
            var deputyDetailNewApi = await _searchDeputyRepository.GetDeputyDetailNewApi(legislatura, id);
            deputiesDetailListNewApi.Add(deputyDetailNewApi);
        }

        var deputiesDetailListOldApi = new List<DeputyDetailOldApi>();
        DeputiesListOldApi deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);
        foreach (var deputy in deputiesListOldApi.DeputiesOldApi)
        {
            var id = Convert.ToInt32(deputy.IdeCadastro);
            var deputyDetailOldApi = await _searchDeputyRepository.GetDeputyDetailOldApi(legislatura, id);
            deputiesDetailListOldApi.Add(deputyDetailOldApi);
        }

        var deputiesDetailList = new DeputiesDetailListDto(deputiesDetailListOldApi, deputiesDetailListNewApi);

        return deputiesDetailList;
    }
    
    public async Task RefreshDeputyDetails(int year)
    {
        var legislaturaObj = Legislatura.CriarLegislaturaPorAno(year);
        var deputiesDetailListDto = await GetDeputiesDetailListExternalApi(legislaturaObj.Numero);
        foreach (DeputyDetailDto deputyDetail in deputiesDetailListDto.DeputiesDetail)
        {
            await _nonRelationalDatabase.CheckAndUpdate(deputyDetail);
        }
    }

    public async Task RefreshNewApi(int year)
    {
        var legislaturaObj = Legislatura.CriarLegislaturaPorAno(year);
        var legislatura = legislaturaObj.Numero;
        _logger.Information($"RefreshNewApi {legislatura} {year}");
        int counter = 0;

        DeputiesListNewApi deputiesListNewApi = await _searchDeputyRepository.GetAllDeputiesNewApi(legislatura);
        var total = deputiesListNewApi.DeputiesNewApi.Count;
        foreach (var deputy in deputiesListNewApi.DeputiesNewApi)
        {
            try
            {
                var id = Convert.ToInt32(deputy.Id);
                _logger.Debug($"starting deputy details new api {id} {counter}");
                DeputyDetailNewApi deputyDetailNewApi =
                    await _searchDeputyRepository.GetDeputyDetailNewApi(legislatura, id);
                await _nonRelationalDatabase.CheckAndUpdate(deputyDetailNewApi);
                var currentMonth = DateTime.Now.Year == year ? DateTime.Now.Month : 12;
                for (int month = 1; month <= currentMonth; month++)
                {
                    _logger.Debug($"getting expenses deputy {id} {counter}/{total}");
                    DeputyExpense deputyExpense = await _searchDeputyRepository.GetDeputyExpense(year, month, id);
                    await _nonRelationalDatabase.CheckAndUpdate(deputyExpense);
                }
                counter++;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error on deputy new api {deputy.Id}");
            }
        }
        _logger.Information("Database RefreshNewApi refreshed");
    }

    public async Task RefreshOldApi(int year)
    {
        var legislaturaObj = Legislatura.CriarLegislaturaPorAno(year);
        var legislatura = legislaturaObj.Numero;
        _logger.Information($"RefreshNewApi {legislatura} {year}");
        int counter = 0;
        
        var deputiesListOldApi = await _searchDeputyRepository.GetAllDeputiesOldApi(legislatura);
        var total = deputiesListOldApi.DeputiesOldApi.Count;
        foreach (var deputy in deputiesListOldApi.DeputiesOldApi)
        {
            try
            {
                var id = deputy.IdeCadastro;
                var matricula = deputy.Matricula;
                _logger.Debug($"starting deputy details old api {id} {counter}/{total}");
                DeputyDetailOldApi deputyDetailOldApi =
                    await _searchDeputyRepository.GetDeputyDetailOldApi(legislatura, id);
                await _nonRelationalDatabase.CheckAndUpdate(deputyDetailOldApi);
                var currentMonth = DateTime.Now.Year == year ? DateTime.Now.Month : 12;
                for (int month = 1; month <= currentMonth; month++)
                {
                    _logger.Debug($"getting work presence deputy {matricula} {counter}/{total}");
                    var deputyWorkPresence =
                        await _searchDeputyRepository.GetDeputyWorkPresence(year, month, matricula);
                    await _nonRelationalDatabase.CheckAndUpdate(deputyWorkPresence);
                }
                counter++;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error on deputy old api {deputy.IdeCadastro}");
            }
        }
        _logger.Information("Database RefreshOldApi refreshed");
    }
    
    public async Task RefreshAllMongoDb(int year)
    {
        await RefreshNewApi(year);
        await RefreshOldApi(year);
    }
    
    public async Task RefreshRelationalDbFromNonRelationalDb(int year)
    {
        DeputyDetailDto currentDeputy = null;
        DeputyDomain currentDeputyDomain = null;
        DeputyExpense currentExpense = null;
        try
        {
            _logger.Information($"RefreshRelationalDbFromNonRelationalDb {year}");

            var legislaturaObj = Legislatura.CriarLegislaturaPorAno(year);
            IEnumerable<DeputyDetailDto> deputiesDetailDtos = 
                await _nonRelationalDatabase.GetAll<DeputyDetailDto>(a => a.IdLegislatura == legislaturaObj.Numero);

            foreach (DeputyDetailDto deputyDetailDto in deputiesDetailDtos)
            {
                currentDeputy = deputyDetailDto;
                currentDeputyDomain = DeputyDetailDto.GetDeputyDomainFromDto(deputyDetailDto);
                var deputyEntity = DeputyMapper.MapToDeputado(currentDeputyDomain);
                var expenses = await _nonRelationalDatabase.GetAll<DeputyExpense>(
                    a => a.HasData && a
                        .IdDeputy == deputyDetailDto.IdDeputy && a.Ano == year);
                if(expenses.Count == 0)
                    continue;
                foreach (var expense in expenses)
                {
                    try
                    {
                        currentExpense = expense;
                        var expenseDto = DeputyExpenseDto.GetDtoFromMongo(expense);
                        var expenseDomain = DeputyExpenseMapper.MapToExpense(expenseDto);
                        var supplierDomain = expenseDomain.Supplier;
                        var supplierEntity = SupplierMapper.MapToEntity(supplierDomain);
                        var expenseEntity = DeputyExpenseMapper.MapToDeputyExpense(expenseDomain);

                        var supplierItem = _unitOfWork.SupplierRepository.Get(a => a.Cnpj == supplierEntity.Cnpj ||
                            a.Cpf == supplierEntity.Cpf);
                        if (supplierItem == null)
                        {
                            _unitOfWork.SupplierRepository.Add(supplierEntity);
                            expenseEntity.Supplier = supplierEntity;
                        }
                        else
                        {
                            expenseEntity.Supplier = supplierItem;
                        }

                        if (supplierItem.Cnpj == null)
                        {
                            var personDomain = PersonDomain.CreateSimplePerson(supplierItem.Name, supplierItem.Cpf);
                            var personEntity = Mapper.Mapper.MapToPerson(personDomain);
                            _unitOfWork.PersonRepository.UpdateInsert(personEntity, a => a.Cpf == personEntity.Cpf);
                        }
                        else
                        {
                            var companyDomain = CompanyDomain.CreateCompany(supplierItem.Name, supplierItem.Cnpj);
                            var companyEntity = Mapper.Mapper.MapToCompany(companyDomain);
                            _unitOfWork.CompanyRepository.UpdateInsert(companyEntity, a => a.Cnpj == companyEntity.Cnpj);
                        }
                        _unitOfWork.DeputyExpenseRepository.UpdateInsert(expenseEntity,
                            a => a.IdDocument == expenseEntity.IdDocument);
                        deputyEntity.DeputyExpenses.Add(expenseEntity);
                        _unitOfWork.DeputyRepository.UpdateInsert(deputyEntity, a => a.Cpf == deputyEntity.Cpf);
                        
                    }
                    catch (Exception e)
                    {
                        var message = $"Expense error: {expense} - message: {e.Message}";
                        if (expense.ValorLiquido < THRESHOLD_VALUE)
                        {
                            _logger.Error(message);
                        }
                        else
                        {
                            _logger.Fatal(message);
                            // write the expense in another table for manual analysis
                        }
                    }
                }
                await _unitOfWork.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"An error occurred while refreshing the database from the non-relational database for the year {year}: {currentDeputy} {currentExpense} {ex.Message}");
            throw;
        }
    }
}