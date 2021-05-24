using PirateCave.Base;
using PirateCave.Controllers;
using PirateCave.Enums;
using PirateCave.Models;
using UnityEngine;

namespace PirateCave.Account.Controllers
{
    public class PlayerHistoryController : MonoBehaviour
    {
        public void verifyIfHavePointsToSave()
        {
            int pointsToSave = PlayerPrefs.GetInt(PlayerPrefsKeys.PointsToSave);

            if (pointsToSave == 0)
                return;
            
            saveScore(pointsToSave);
        }

        /// <summary>
        /// Salva os dados os pontos do usuário
        /// </summary>
        public void saveScore(int points)
        {
            if (string.IsNullOrEmpty(GameController.loggedPlayer?.nick)) 
                return;

            PlayerHistory playerHistory = new PlayerHistory()
            {
                nick = GameController.loggedPlayer.nick,
                points = points,
            };

            PlayerPrefs.SetInt(PlayerPrefsKeys.PlayerPoints, points);
            StartCoroutine(Request.put("/history", playerHistory, handleResponse));
            
            // Por garantia já colocamos os pontos para salvar depois
            // caso de algum erro ao se comunicar com o servidor
            PlayerPrefs.SetInt(PlayerPrefsKeys.PointsToSave, points);
        }

        private void handleResponse(Response response)
        {
            if (response != null && !string.IsNullOrEmpty(response.data?.message))
            {
                // Se deu tudo certo, não será necessário salvar os pontos depois
                PlayerPrefs.DeleteKey(PlayerPrefsKeys.PointsToSave);
                return;
            }
        }
    }
}