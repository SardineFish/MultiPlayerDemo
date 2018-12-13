using UnityEngine;
using System.Collections;

public class Singleton <T>: MonoBehaviour where T:Singleton<T>
{
    public static T Instance;
    public Singleton()
    {
        Instance = this as T;
    }
}
