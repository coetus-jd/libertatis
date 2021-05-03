using UnityEngine;
using UnityEngine.SceneManagement;

namespace PirateCave.Controllers
{
    public static class GameController
    {
        /// <summary>
        /// O som que irá tocar de background durante o jogo
        /// </summary>
        [SerializeField]
        private static AudioSource backgroundSound;

        /// <summary>
        /// Carrega uma Scene por seu caminho
        /// </summary>
        /// <param name="sceneName"></param>
        public static void playScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, sceneMode);
        }

        /// <summary>
        /// Carrega uma Scene por seu index
        /// </summary>
        /// <param name="sceneIndex"></param>
        public static void playScene(int sceneIndex, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneIndex, sceneMode);
        }

        /// <summary>
        /// Fecha a aplicação do jogo
        /// </summary>
        public static void quitGame() => Application.Quit();

        /// <summary>
        /// Desloga o usuário atual
        /// </summary>
        public static void logout() => PlayerPrefs.DeleteKey("player");
    }
}

