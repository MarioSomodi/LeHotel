namespace LeHotel.Domain.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId>
            where TId : notnull
    {
        protected AggregateRoot(TId id)
            : base(id)
        { }
        /// <summary>
        /// Needed for instancing in ef core
        /// </summary>
#pragma warning disable CS8618
        protected AggregateRoot()
        {
        }
#pragma warning restore CS8618 
    }
}
