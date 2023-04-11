using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float delayTime = 3f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LoadNextLevel(currentScene + 1));
        }        
    }

    IEnumerator LoadNextLevel(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        if (sceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScene();

        SceneManager.LoadScene(sceneIndex);        
    }
}
