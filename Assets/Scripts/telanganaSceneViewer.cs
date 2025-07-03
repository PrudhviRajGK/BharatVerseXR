
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class telanganaSceneViewer : MonoBehaviour
{
    [Header("Scene name to load")]
    [SerializeField] private string sceneToLoad = "telanganaScene";

    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("telanganaSceneViewer: No scene name assigned in Inspector.");
        }
    }
}
