using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class rajasthanSceneSwitcher : MonoBehaviour
{
    [Header("Scene name to load")]
    [SerializeField] private string sceneToLoad = "rajasthanScene";

    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("rajasthanSceneSwitcher: No scene name assigned in Inspector.");
        }
    }
}
