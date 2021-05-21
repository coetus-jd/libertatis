using PirateCave.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PirateCave.Controllers
{
    public class GameController : MonoBehaviour
    {
        /// <summary>
        /// Retorna os dados do usuário que está logado atualmente
        /// </summary>
        /// <value></value>
        public static Player loggedPlayer
        {
            get 
            {
                string playerJson = PlayerPrefs.GetString("player");

                if (string.IsNullOrEmpty(playerJson))
                    return null;

                return JsonUtility.FromJson<Player>(playerJson);
            }
        }

        /// <summary>
        /// O som que irá tocar de background durante o jogo
        /// </summary>
        [SerializeField]
        private AudioSource backgroundSound;

        /// <summary>
        /// Carrega uma Scene por seu caminho
        /// </summary>
        /// <param name="sceneName"></param>
        public void playScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

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
        public void quitGame() => Application.Quit();

        /// <summary>
        /// Desloga o usuário atual
        /// </summary>
        public void logout() => PlayerPrefs.DeleteKey("player");
    }
}

