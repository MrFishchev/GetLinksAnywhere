namespace GetLinksAnywhere.Common
{
    public interface IBuilder<T>
    where T : class
    {
        T Build();
    }
}
