namespace DAL.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }
}
