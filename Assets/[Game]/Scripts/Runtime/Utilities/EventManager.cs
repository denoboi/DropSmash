using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //Use this event manager for your custom ingame events.

    public static UnityEvent<Vector3, Color> OnRevealed = new UnityEvent<Vector3, Color>();


}
