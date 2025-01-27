namespace Deputies.Domain.AbstractEntities;

public abstract class Participant
{
    public abstract string DisplayName { get; }

    public override abstract bool Equals(object obj);
    public override abstract int GetHashCode();
}