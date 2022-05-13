namespace TaghcheCaching.InfraStructure.Interface.Caching
{
    public interface IInMemoryCacheService
    {
        public void SetInMemory(int id, object book);
        public object? GetFromMemory(int id);

    }
}