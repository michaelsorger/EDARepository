//Michael Sorger code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGunnerBullet : MonoBehaviour
{

    float lifeTime = 1.0f;
    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      //  Debug.Log("Parent of this " + gameObject + " is " + gameObject.transform.parent.gameObject);
        Destroy(gameObject.transform.parent.gameObject);
        //Destroy(gameObject.transform.root.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log("Parent of this " + gameObject + " is " + gameObject.transform.parent.gameObject);
        Destroy(gameObject.transform.parent.gameObject);
        //Destroy(gameObject.transform.root.gameObject);
    }
}
