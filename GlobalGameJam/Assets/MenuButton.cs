using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnSubmit(BaseEventData data)
    {
        Debug.Log("YES");
    }

    public void Play()
    {
        SceneManager.LoadScene("Final");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
