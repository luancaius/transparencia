namespace Deputies.Domain.AbstractEntities
{
    public abstract class Participant
    {
        /// <summary>
        /// A display name or identifier for this participant.
        /// </summary>
        public abstract string DisplayName { get; }

        public override abstract bool Equals(object obj);
        public override abstract int GetHashCode();
    }
}