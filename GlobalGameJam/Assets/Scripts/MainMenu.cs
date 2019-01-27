using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool menuSelection = true;

    public Text startText;
    public Text quitText;

    public AudioSource audioPlayer;
    public AudioClip menuBeep;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && menuSelection)
        {
            menuSelection = false;

            audioPlayer.PlayOneShot(menuBeep);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)&& !menuSelection)
        {
            menuSelection = true;

            audioPlayer.PlayOneShot(menuBeep);
        }
    }

    void OnGUI()
    {
        if (menuSelection)
        {
            startText.text = ">START";
            quitText.text = "  QUIT";
        }else
        {
            startText.text = "  START";
            quitText.text = ">QUIT";
        }

    }
}
