using System;
using PirateCave.Base;

namespace PirateCave.Models
{
    /// <summary>
    /// Representa um jogador e seus dados
    /// </summary>
    [Serializable]
    public class Player : Model
    {
        public string nick;
        public string name;
    }
}