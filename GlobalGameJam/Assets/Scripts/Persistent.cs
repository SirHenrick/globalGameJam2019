using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This will ensure that the current object will be persistent across scenes.
/// </summary>
public abstract class Persistent<T> : MonoBehaviour
{
    public static T instance;

    protected void Awake()
    {
        //DontDestroyOnLoad(this);
        instance = gameObject.GetComponent<T>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
}


