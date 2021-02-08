public interface IPoolable
{
    void OnObjectCreated();

    void OnObjectReused();
}