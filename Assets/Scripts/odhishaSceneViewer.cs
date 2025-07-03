
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class odhishaSceneViewer : MonoBehaviour
{
    [Header("Scene name to load")]
    [SerializeField] private string sceneToLoad = "odhishaScene";

    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("odhishaSceneViewer: No scene name assigned in Inspector.");
        }
    }
}
