using System;
using PirateCave.Account.Controllers;
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

        [SerializeField]
        /// <summary>
        /// Objeto usado para atualizar os pontos do usuário
        /// </summary>
        private PlayerHistoryController historyController;
        
        void Start()
        {
            points = PlayerPrefs.GetInt("playerHistory");
        }

        void OnDisable()
        {
            PlayerPrefs.SetInt("pointsToSave", points);
        }

        void OnApplicationQuit()
        {
            historyController.saveScore(points);
        }
    }
}
