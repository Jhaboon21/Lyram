using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }
        if(weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)
        {
            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                Debug.Log("Flute");
                break;
            case 2:
                Debug.Log("Horn");
                break;
            case 3:
                Debug.Log("Clarinet");
                break;
            case 4:
                Debug.Log("Cello");
                break;
            case 5:
                Debug.Log("Lyram");
                break;
        }
    }
}
