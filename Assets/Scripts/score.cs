using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    public AudioSource audioCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ScoreManager.score++;
            Destroy(gameObject);
            audioCoin.Play();
        }
    }
}
