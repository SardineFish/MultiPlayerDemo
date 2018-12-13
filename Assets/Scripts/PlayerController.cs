using UnityEngine;
using System.Collections;

public abstract class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 10;
    public float MaxTrunSpeed = 360;
    public virtual void Move(Vector2 movement)
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position.ToVector2() + movement * MaxSpeed);
    }
    public virtual void Aim(Vector2 direction)
    {

    }
}
