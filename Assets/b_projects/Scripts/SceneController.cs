using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public void OpenScene(string name, bool additive = true)
    {
        SceneManager.LoadScene(name, additive ? LoadSceneMode.Additive :LoadSceneMode.Single);
    }

    public void CloseScene(string name)
    {
        Scene sceneToClose = SceneManager.GetSceneByName(name);

        if (sceneToClose != null)
        {
            SceneManager.UnloadSceneAsync(sceneToClose);
        }
    }
}
