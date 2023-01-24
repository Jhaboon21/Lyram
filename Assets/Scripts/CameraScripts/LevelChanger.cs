using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator anim;
    public Animator musicAnim;
    [SerializeField] private float waitTime;
    [SerializeField] private DoorScript doorScript;
    [SerializeField] private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        if (doorScript.leavingScene == true)
            FadeToLevel(2);
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
    IEnumerator ChangeScene()
    {
        musicAnim.SetTrigger("MusicFadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelToLoad);
    }
}
