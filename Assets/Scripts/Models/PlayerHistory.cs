using System;
using PirateCave.Base;

namespace PirateCave.Models
{
    /// <summary>
    /// Representa o histórico de um jogador
    /// </summary>
    [Serializable]
    public class PlayerHistory : Model
    {
        public int id;
        public string nick;
        public string date;
        public int points;
    }
}