

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal interface ISerializer<T> where T : class
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
