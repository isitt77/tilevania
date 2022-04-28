using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPickUp : MonoBehaviour
{

    [SerializeField] AudioClip coinSFX;
    [SerializeField] int pointsForCoinPickup = 100;
    //int coinScore;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            //GetCoinScore();
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }   
    }

    //void GetCoinScore()
    //{
    //    coinScore+=1;
    //    Debug.Log(coinScore);
    //}
}
