using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int coinPickUpScore = 25;
    [SerializeField] float audioVolume = .15f;

    bool coinWasCollected = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !coinWasCollected) {
            coinWasCollected = true; //to ensure coin doesnt get picked up twice prevent potential bugs
            FindObjectOfType<GameSession>().AddToScore(coinPickUpScore);
            //play at camera position due to 2d and in 3d space the camera/output audio is in a differ position from the output sound
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position, audioVolume); 
            Destroy(gameObject);
        }
    }
}
