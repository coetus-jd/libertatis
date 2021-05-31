using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PirateCave.Resources
{
    /// <summary>
    /// Todos os textos que são necessários na interface do jogo
    /// </summary>
    public static class Resources
    {
        public static Dictionary<string, Manuscript> Manuscripts
        {
            get
            {
                if (_Manuscripts != null && Manuscripts.Count > 0)
                    return _Manuscripts;

                string path = Path.Combine(Application.dataPath, "Scripts/Resources");

                _Manuscripts = new Dictionary<string, Manuscript>()
                {
                    ["pt-BR"] = JsonUtility.FromJson<Manuscript>(File.ReadAllText($"{path}/pt-BR.json")),
                    ["en-US"] = JsonUtility.FromJson<Manuscript>(File.ReadAllText($"{path}/en-US.json")),
                };

                return _Manuscripts;
            }
        }

        private static Dictionary<string, Manuscript> _Manuscripts;
    }

    [Serializable]
    public class Manuscript
    {
        public List<string> texts;
    }
}