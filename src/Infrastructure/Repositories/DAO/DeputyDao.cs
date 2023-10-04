namespace Repositories.DAO;

public class DeputyDao : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string DateOfBirth { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string CPF { get; private set; }
    public string Gender { get; private set; }
    public List<LegislaturaDao> Legislaturas { get; private set; }
    public string Nickname { get; private set; }
}