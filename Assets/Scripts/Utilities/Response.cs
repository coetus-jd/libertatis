using System;

namespace PirateCave.Utilities
{
    [Serializable]
    public class Response
    {
        public Data data;
    }

    [Serializable]
    public class Data
    {
        public string error;
        public string message;
    }
}