using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private bool m_isGameOverScreen = false;

	private void OnEnable()
	{
		if (m_isGameOverScreen)
        {
            MiscUtils.IsGameOver = true;
        }
	}

	public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame() {
        Application.Quit();
        Debug.Log("Game has Quit");
    }
}
