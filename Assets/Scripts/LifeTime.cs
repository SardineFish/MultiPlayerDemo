using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour
{
    public float lifeTime = 1;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
