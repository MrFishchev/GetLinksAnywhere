namespace GetLinksAnywhere.Common.Interfaces
{
    public interface IBuilder<T>
    where T : class
    {
        T Build();
    }
}
