using System;
using PirateCave.Base;
using PirateCave.Controllers;
using PirateCave.Enums;
using PirateCave.Models;
using TMPro;
using UnityEngine;

namespace PirateCave.Controllers.Account
{
    public class PlayerHistoryController : MonoBehaviour
    {
        /// <summary>
        /// Corpo da tabela aonde será exibido os dados
        /// </summary>
        [SerializeField]
        private GameObject tableBody;

        /// <summary>
        /// Prefab com os dados a serem exibidos na tabela
        /// </summary>
        [SerializeField]
        private GameObject rowPrefab;

        void Start()
        {
            getLastPoints();
        }

        public void getLastPoints()
        {
            if (string.IsNullOrEmpty(GameController.loggedPlayer?.nick))
                return;

            StartCoroutine(Request.get($"/history/{GameController.loggedPlayer.nick}", handleGetPointsResponse));
        }

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
            StartCoroutine(Request.put("/history", playerHistory, handleSaveScoreResponse));

            // Por garantia já colocamos os pontos para salvar depois
            // caso de algum erro ao se comunicar com o servidor
            PlayerPrefs.SetInt(PlayerPrefsKeys.PointsToSave, points);
        }


        private void handleGetPointsResponse(Response response)
        {
            if (response.data.history.Count == 0)
                return;
            
            response.data.history.ForEach((history) =>
            {
                GameObject row = Instantiate(rowPrefab, tableBody.transform);

                var rowDatas = row.GetComponentsInChildren<TextMeshProUGUI>();
                rowDatas[0].text = history.points.ToString();
                rowDatas[1].text = Convert.ToDateTime(history.date).ToString("dd/MM/yyyy");
            });
        }

        private void handleSaveScoreResponse(Response response)
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