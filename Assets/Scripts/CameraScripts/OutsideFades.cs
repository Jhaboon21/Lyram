using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutsideFades : MonoBehaviour
{
    public Animator anim;
    public Animator musicAnim;
    public GameObject barDoor;
    public GameObject nextLevel;
    [SerializeField] private float waitTime;
    [SerializeField] private int levelToLoad;
    public int NextLevelIndex;
    public bool playerIsClose = false;
    [SerializeField] private bool openDoor;

    void Start()
    {
        openDoor = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && openDoor == false && playerIsClose)
        {
            FadeToLevel(NextLevelIndex);
            openDoor = true;
        }
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        StartCoroutine(ChangeScene());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
    IEnumerator ChangeScene()
    {
        musicAnim.SetTrigger("MusicFadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelToLoad);
    }
}
