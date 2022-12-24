using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    //private Rigidbody2D rb;
    //private SpriteRenderer sprRend;
    PlayerLife playerlife;
    public GameObject player;
    private float percent;
    private Vector3 originalScale;

    /*
    void Awake()
    {
        sprRend = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        //sprRend.drawMode = SpriteDrawMode.Sliced;

        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    
    */
    void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("./bubble.png");
        //gameObject.transform.Translate(0.0f, 0.0f, 0.0f);

        playerlife = player.GetComponent<PlayerLife>();
        originalScale = transform.localScale;

    }
    

    void Update()
    {

        percent = playerlife.currentHealth / 100;
        transform.localScale = originalScale * percent;
        //Debug.Log(percent);
        //Debug.Log(transform.localScale);
       
        
    }
}
