using System;
using PirateCave.Account.Controllers;
using PirateCave.Base;
using PirateCave.Enums;
using PirateCave.Models;
using UnityEngine;

namespace PirateCave.Controllers
{
    public class PhaseController : MonoBehaviour
    {
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
        
        void Start()
        {
            points = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayerPoints);
        }

        void OnDisable()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.PointsToSave, points);
        }

        void OnApplicationQuit()
        {
            historyController.saveScore(points);
        }
    }
}
