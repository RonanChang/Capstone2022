using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueController : MonoBehaviour
{

    [SerializeField] private GameObject DialogPanel;
   //[SerializeField] private GameObject SelectPanel;
    [SerializeField] private Text text;
    [SerializeField] private string[] content;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private Text buttonText;
    [SerializeField] private GameObject indicator;
    private int index;

    [SerializeField] private float wordSpeed;
    private bool playerIsClose;

  
    void Update()
    {

        if (playerIsClose) { 
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {

            
            if (DialogPanel.activeInHierarchy)
            {
                noText();
            }
            else
            {
                DialogPanel.SetActive(true);
               
                StartCoroutine(Typing());
            }
        }

        if(text.text == content[index])
        {
            button.SetActive(true);
            buttonText.text = "continue";
            if (index == content.Length - 1)
            {
                buttonText.text = "exit";
                readyButton.SetActive(true);
            }
            
        }
        else
        {
            button.SetActive(false);
            readyButton.SetActive(false);

        }
    }

    public void noText()
    {
        text.text = "";
        index = 0;
        DialogPanel.SetActive(false);
    }

    IEnumerator Typing() {
        foreach(char letter in content[index].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        button.SetActive(false);
        //Debug.Log("here");
        

        if (index < content.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            noText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            playerIsClose= true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            noText();
        }
    }
}
