using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneOnClick : MonoBehaviour 
{

    public string sceneName = "MainScene";

	#region Methods

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

	#endregion

}
