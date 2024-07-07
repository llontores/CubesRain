using UnityEngine.Events;

public interface IDisableable<T>
{
    public event UnityAction<T> Disabled; 
}
