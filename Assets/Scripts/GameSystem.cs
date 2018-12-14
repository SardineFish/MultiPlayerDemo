using UnityEngine;
using System.Collections;

public class GameSystem : Singleton<GameSystem>
{
    public int Tick = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Tick++;
    }
}
