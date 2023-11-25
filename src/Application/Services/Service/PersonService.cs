using Services.Interfaces;

namespace Services.Service;

public class PersonService : IPersonService
{
    public Task RefreshPersonTableFromMongo()
    {
        // get deputy details from mongo
        // for each, check if exists in relational repository
        // if not, add to relational repository
        // if exists, check if is updated
        // if not, update
        return Task.CompletedTask;
    }
}

