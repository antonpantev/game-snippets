using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolbox
{
    public class StartMenu : MonoBehaviour
    {
        public string playScene;

        public void PlayGame()
        {
            SceneManager.LoadScene(playScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}