using System;
using System.Collections.Generic;
using PirateCave.Models;

namespace PirateCave.Base
{
    /// <summary>
    /// Classe utiliatária para tratar a resposta vinda do servidor
    /// </summary>
    [Serializable]
    public class Response
    {
        public Data data;
    }

    /// <summary>
    /// Classe para armazenar as mensagens retornados na resposta
    /// </summary>
    [Serializable]
    public class Data
    {
        public string error;
        public string message;
        public Player player;
        public int todayPoints;
        public List<PlayerHistory> history;
    }
}