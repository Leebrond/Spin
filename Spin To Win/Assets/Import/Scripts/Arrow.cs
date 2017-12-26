using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public AudioSource soundArrow;

    void Start()
    {
        soundArrow = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        soundArrow.PlayOneShot(soundArrow.clip);
    }
}
