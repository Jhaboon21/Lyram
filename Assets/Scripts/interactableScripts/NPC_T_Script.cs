using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NPC_T_Script : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public GameObject darlynImage;
    public GameObject npcImage;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 10)]
    public string[] dialogue;
    private int _index;
    public bool hasTalkedToT = false;
    public float wordSpeed;
    public bool playerIsClose;

    void Update()
    {
        if (dialogueText.text == dialogue[_index])
            continueButton.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                hasTalkedToT = true;
                dialoguePanel.SetActive(true);
                npcImage.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(dialogueText.text == dialogue[_index])
        {
            continueButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        _index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[_index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if (_index < dialogue.Length - 1)
        {
            _index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            continueButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
