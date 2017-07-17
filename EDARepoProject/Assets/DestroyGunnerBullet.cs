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
}
