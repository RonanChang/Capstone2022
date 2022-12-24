using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int lightCount = 0;
    private Rigidbody2D player;
    [SerializeField] private Text lightCountText;

    [SerializeField] private AudioSource collectingSound;

    private PlayerLife playerlife;

    private void Start()
    {
        player= GetComponent<Rigidbody2D>();
        playerlife = player.GetComponent<PlayerLife>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light")) {
            collectingSound.Play();
            //remove it once collected
            Destroy(collision.gameObject);

            lightCount++;

            //increase player health when get a light
            playerlife.currentHealth += 20;


            //Debug.Log("light count is:" + lightCount);
            lightCountText.text = "light count: " + lightCount;
        }
    }
}
