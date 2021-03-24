using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public AudioClip dropClip;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        source.clip = dropClip;
        source.Play();
    }
}
