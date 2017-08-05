using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeOf(ParticleSystem))]

public class ParticleStarter : MonoBehaviour
{

    private ParticleSystem _psystem;

    void Awake()
    {
        _psystem = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider col)
    {
        _psystem.Play();
    }



// Update is called once per frame
void Update () {
		
	}
}
