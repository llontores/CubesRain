using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public interface IDisableable<T>
{
    public event UnityAction<T> Disabled; 
}
