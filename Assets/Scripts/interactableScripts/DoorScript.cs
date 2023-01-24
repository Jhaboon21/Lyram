using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] public bool playerIsClose = false;
    [SerializeField] public bool openDoor = false;
    public bool leavingScene = false;
    public NPC_T_Script npcT;

    public GameObject dialoguePanel;
    public GameObject continueButton;
    public GameObject npcImage;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 10)]
    public string[] dialogue;
    private int _index;
    public float wordSpeed;

    void Update()
    {
        if (dialogueText.text == dialogue[_index])
            continueButton.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E) && openDoor == false && playerIsClose)
        {
            Debug.Log("nah");
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                npcImage.SetActive(false);
                StartCoroutine(Typing());
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && openDoor == true && playerIsClose)
        {
            Debug.Log("ok");
            GetComponent<BoxCollider2D>().enabled = false;
            leavingScene = true;
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
        foreach (char letter in dialogue[_index].ToCharArray())
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
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playerIsClose = true;
            if (npcT.hasTalkedToT == false)
            {
                Debug.Log("Cannot pass");
            }
            else if (npcT.hasTalkedToT == true)
            {
                Debug.Log("You can pass");
                openDoor = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
