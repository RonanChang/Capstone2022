using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    private Rigidbody2D player;
    [SerializeField] public float maxHealth = 150f;
    [SerializeField] public float currentHealth = 100f;

    [SerializeField] private Text playerHealthText;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerHealthText.text = "Health:" + currentHealth;

        //check if the player's dead
        if (currentHealth <= 0f)
        {
            Die();
            RestartLevel();
        }
        else if(currentHealth > 0){
            //health is decreasing based on the floor
            //Debug.Log("Here");
            if(transform.position.y> -2f){
                //Debug.Log("On the 1 floor");
                currentHealth -= 0.001f;
            }
            else if (transform.position.y < -2f)
            {
               // Debug.Log("On the 2st floor");
                currentHealth -= 0.002f;
            }else if(transform.position.y < -10f)
            {
                //Debug.Log("On the 3nd floor");
                currentHealth -= 0.004f;
            }
            
            
        }

        if (currentHealth > maxHealth) { 
            currentHealth= maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Saw"))
        {

            currentHealth -= 5f;

            //Die();
            //RestartLevel();
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) {

            currentHealth -= 10f;
            
            //Die();
            //RestartLevel();
        }
    }

    

    private void Die()
    {
        Debug.Log("Player is dead");

        //disable player movement
        player.bodyType = RigidbodyType2D.Static;

    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
