namespace InternetForum.Domain.Interfaces
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}
