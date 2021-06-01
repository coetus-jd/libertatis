using System;
using PirateCave.Controllers.Account;
using PirateCave.Enums;
using PirateCave.UI;
using TMPro;
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
        /// Guarda o número de pontos feitos pelo jogador
        /// </summary>
        private int points;

        /// <summary>
        /// Objeto usado para atualizar os pontos do usuário
        /// </summary>
        [SerializeField]
        private PlayerHistoryController historyController;

        [SerializeField]
        private AudioSource backgroundAudio;
        
        [Header("UI")]
        /// <summary>
        /// Texto da UI aonde será exibido os pontos
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI pointsText;

        /// <summary>
        /// Painel aonde será exibido a mensagem de que o jogador perdeu
        /// </summary>
        public GameObject youLosePanel;

        /// <summary>
        /// Menu que será exibido quando o jogador pausar o game
        /// </summary>
        [SerializeField]
        private GameObject pauseMenu;
        
        void Start()
        {
            points = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayerPoints);
            backgroundAudio.Play();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                togglePauseGame();
        }

        void FixedUpdate()
        {
            pointsText.text = points.ToString();
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

        public void togglePauseGame()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = pauseMenu.activeSelf ? 0f : 1f;
        }
    }
}
