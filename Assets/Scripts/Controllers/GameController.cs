using UnityEngine;
using UnityEngine.SceneManagement;

namespace PirateCave.Controllers
{
    public class GameController : MonoBehaviour
    {
        /// <summary>
        /// O som que irá tocar de background durante o jogo
        /// </summary>
        [SerializeField]
        private AudioSource backgroundSound;

        /// <summary>
        /// Carrega uma Scene por seu caminho
        /// </summary>
        /// <param name="sceneName"></param>
        public void playScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, sceneMode);
        }

        /// <summary>
        /// Carrega uma Scene por seu index
        /// </summary>
        /// <param name="sceneIndex"></param>
        public void playScene(int sceneIndex, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneIndex, sceneMode);
        }

        /// <summary>
        /// Fecha a aplicação do jogo
        /// </summary>
        public void quitGame()
        {
            Application.Quit();
        }
    }
}

