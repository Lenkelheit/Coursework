namespace Core.Interfaces
{
    public interface IFactory<in TKey, in TRegValue, out TReturnValue>
    {
        void Registrate(TKey key, TRegValue value);
        void UnRegistrate(TKey key);
        TReturnValue MakeInstance(TKey key);
    }
}
