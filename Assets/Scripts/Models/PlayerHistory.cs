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
        public UInt32 id;
        public string nick;
        public DateTime date;
        public int points;
    }
}