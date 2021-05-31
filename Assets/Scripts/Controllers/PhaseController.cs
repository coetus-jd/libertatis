using System;
using PirateCave.Controllers.Account;
using PirateCave.Enums;
using PirateCave.UI;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PhaseController : MonoBehaviour
    {
        /// <summary>
        /// Painel aonde será exibido os manuscritos
        /// </summary>
        public ManuscriptPanel manuscriptPanel;

        /// <summary>
        /// Painel aonde será exibido a mensagem de que o jogador perdeu
        /// </summary>
        public GameObject youLosePanel;

        /// <summary>
        /// Guarda o número de pontos feitos pelo jogador
        /// </summary>
        [SerializeField]
        private int points;

        /// <summary>
        /// Objeto usado para atualizar os pontos do usuário
        /// </summary>
        [SerializeField]
        private PlayerHistoryController historyController;

        [SerializeField]
        private AudioSource backgroundAudio;
        
        void Start()
        {
            points = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayerPoints);
            backgroundAudio.Play();
        }

        void OnDisable()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.PointsToSave, points);
        }

        void OnApplicationQuit()
        {
            historyController.saveScore(points);
        }

        public void addPoints(int points)
        {
            points += points;
        }
    }
}
