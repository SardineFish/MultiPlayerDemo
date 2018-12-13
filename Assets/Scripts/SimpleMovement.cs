using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour
{
    public Vector2 velocity;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}
