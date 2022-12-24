using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BreakBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubblesizeText;
    [SerializeField] private GameObject dialogue;
    private Vector3 subtractSize = new Vector3(0.0001f, 0.0001f, 0f);
    private Vector3 addSize = new Vector3(0.02f, 0.02f, 0f);
    private Vector3 minSize = new Vector3(0f, 0f, 0f);
    private SpriteRenderer sr;
    private Color color = Color.clear;
    private bool isReady = false;
    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = minSize;
            }
            transform.localScale -= subtractSize;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //Debug.Log("Q is pressed");
                transform.localScale += addSize;
            }

            if (transform.localScale.x >= 4.5f)
            {
                //Debug.Log("The bubble is broken");
                sr.color = color;
            }
        }
    
    }

    public void getReady()
    {
        isReady = true;
        bubblesizeText.SetActive(true);
        dialogue.SetActive(false);
    }
}
