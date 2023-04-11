using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{

    void Awake()
    {
        int scenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (scenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScene()
    {
        Destroy(gameObject);
    }
}
