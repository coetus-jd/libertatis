using System;
using PirateCave.Base;
using PirateCave.Models;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PhaseController : MonoBehaviour
    {
        [SerializeField]
        /// <summary>
        /// Guarda o número de pontos feitos pelo jogador
        /// </summary>
        private int points;

        void Start()
        {
            points = PlayerPrefs.GetInt("playerHistory");
        }

        void OnDestroy()
        {
            saveScore();
        }

        private void saveScore()
        {
            PlayerHistory playerHistory = new PlayerHistory()
            {
                nick = GameController.loggedPlayer.nick,
                points = points,
            };

            PlayerPrefs.SetInt("playerHistory", points);
            StartCoroutine(Request.put("/history", playerHistory, handleSaveScoreResponse));
        }

        private void handleSaveScoreResponse(Response response)
        {

        }
    }
}
