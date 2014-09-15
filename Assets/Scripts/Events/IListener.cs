namespace Assets.Scripts.Events
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}