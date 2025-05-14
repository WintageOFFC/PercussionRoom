using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene switching by index.
/// </summary>
public class SceneSwitcher : MonoBehaviour
{
    /// <summary>
    /// Loads a scene by its build index.
    /// </summary>
    /// <param name="sceneIndex">Index of the scene in the Build Settings.</param>
    public void SwitchToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
