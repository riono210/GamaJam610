using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //Delete GameObject when particle playback is over.
        if (particle.isPlaying == false) Destroy(gameObject);
    }
}
