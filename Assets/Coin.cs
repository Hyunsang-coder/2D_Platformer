using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;
    [SerializeField] AudioClip pickupSFX;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.EarnCoin();
            audioManager.PlayAudio("CoinPickupSFX");
            //AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
