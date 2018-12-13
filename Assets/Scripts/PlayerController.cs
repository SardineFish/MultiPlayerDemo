using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Input;

public abstract class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 10;
    public float MaxTrunSpeed = 360;
    public virtual void Move(Vector2 movement)
    {
        //GetComponent<Rigidbody2D>().MovePosition(transform.position.ToVector2() + movement * MaxSpeed * Time.deltaTime);
        //transform.Translate(movement * MaxSpeed * Time.deltaTime);
        transform.position = transform.position.ToVector2()+ movement * MaxSpeed * Time.deltaTime;
    }
    public virtual void Aim(Vector2 direction)
    {
        if (direction.magnitude > 0)
        {
            var dAng = MathUtility.MapAngle(MathUtility.ToAng(direction) - MathUtility.ToAng(transform.right));
            
            if (Mathf.Abs(dAng) > MaxTrunSpeed * Time.deltaTime)
                dAng = MathUtility.SignInt(dAng) * MaxTrunSpeed * Time.deltaTime;
            var ang = MathUtility.ToAng(transform.right) + dAng - MathUtility.ToAng(Vector2.right);
            GetComponent<Rigidbody2D>().MoveRotation(ang);
        }
    }
    public virtual void Fire()
    {
        GetComponent<Shooter>().Fire();
    }
}
