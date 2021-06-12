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
    public static class Resource
    {
        public static Dictionary<string, AllResources> Language
        {
            get
            {
                if (_Language != null && _Language.Count > 0)
                    return _Language;

                string path = Path.Combine(Application.dataPath, "Scripts/Resources");

                _Language = new Dictionary<string, AllResources>()
                {
                    ["pt-BR"] = JsonUtility.FromJson<AllResources>(File.ReadAllText($"{path}/pt-BR.json")),
                    ["en-US"] = JsonUtility.FromJson<AllResources>(File.ReadAllText($"{path}/en-US.json")),
                };

                return _Language;
            }
        }

        /// <summary>
        /// Váriavel auxiliar, para guardar a referência dos textos
        /// </summary>
        private static Dictionary<string, AllResources> _Language;
    }

    [Serializable]
    public class AllResources
    {
        public Manuscript Manuscript;
        public Corsair Corsair;
    }

    [Serializable]
    public class ResourceBase
    {
        public List<string> texts;
    }

    [Serializable]
    public class Manuscript : ResourceBase {}

    [Serializable]
    public class Corsair : ResourceBase {}
    
    [Serializable]
    public class Dialog1 : ResourceBase {}

    [Serializable]
    public class FinalA : ResourceBase {}

    [Serializable]
    public class FinalB : ResourceBase {}
}