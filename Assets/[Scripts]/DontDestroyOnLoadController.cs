using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadController : MonoBehaviour
{
    private static DontDestroyOnLoadController sInstance;
    public static DontDestroyOnLoadController Instance => sInstance;

    public AudioSource mainTheme;

    void Awake()
    {

        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            sInstance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        mainTheme.Play();    
    }
}
