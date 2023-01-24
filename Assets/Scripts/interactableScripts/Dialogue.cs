using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    /*public string name;
    [TextArea(3,10)]
    public string[] sentences;*/

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public DoorScript door;

    void Start()
    {
        //StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
            continueButton.SetActive(true);
        if(door.playerIsClose == true && door.openDoor == true)
        {
            StartCoroutine(Type());
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
}
