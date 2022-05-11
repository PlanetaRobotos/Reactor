using System.Collections;
using UnityEngine.Events;

namespace _Project.Scripts.Architecture.Services.UIStuff
{
    public class UIEvent : UnityEvent<Hashtable>
    {
    }
    
    public enum UIEventType
    {
        GameStartedAction,
    }
}