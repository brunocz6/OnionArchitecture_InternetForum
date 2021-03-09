namespace InternetForum.Application.Common.Interfaces
{
    public interface IIdentifiable<T> where T : struct
    {
        T Id { get; set; }
    }
}
