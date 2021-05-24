using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using PirateCave.Enums;
using PirateCave.Models;
using PirateCave.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace PirateCave.Base
{
    /// <summary>
    /// Classe utilitária para realizar a comunicação com o servidor
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// Envia uma request do tipo POST ao servidor
        /// </summary>
        /// <param name="route"></param>
        /// <param name="data"></param>
        /// <param name="handleResponse"></param>
        /// <returns></returns>
        public static IEnumerator post(string routeName, dynamic data, Action<Response> handleResponse)
        {
            using (UnityWebRequest request = new UnityWebRequest($"{Helpers.apiUrl}/{handleRouteName(routeName)}", HttpMethods.POST))
            {
                string jsonFields = JsonUtility.ToJson(data);

                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonFields));
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");

                yield return request.SendWebRequest();

                Response apiReponse = JsonUtility.FromJson<Response>(request.downloadHandler?.text);

                handleResponse(apiReponse);
            }
        }
        
        /// <summary>
        /// Envia uma request do tipo GET ao servidor
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="handleResponse"></param>
        /// <returns></returns>
        public static IEnumerator get(string routeName, Action<Response> handleResponse)
        {
            using (UnityWebRequest request = new UnityWebRequest($"{Helpers.apiUrl}/{handleRouteName(routeName)}", HttpMethods.GET))
            {
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");

                yield return request.SendWebRequest();

                Response apiReponse = JsonUtility.FromJson<Response>(request.downloadHandler?.text);

                handleResponse(apiReponse);
            }
        }

        /// <summary>
        /// Envia uma request do tipo PUT ao servidor
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="data"></param>
        /// <param name="handleResponse"></param>
        /// <returns></returns>
        public static IEnumerator put(string routeName, dynamic data, Action<Response> handleResponse)
        {
            using (UnityWebRequest request = new UnityWebRequest($"{Helpers.apiUrl}/{handleRouteName(routeName)}", HttpMethods.PUT))
            {
                string jsonFields = JsonUtility.ToJson(data);

                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonFields));
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");

                yield return request.SendWebRequest();

                Response apiReponse = JsonUtility.FromJson<Response>(request.downloadHandler?.text);

                handleResponse(apiReponse);
            }
        }

        private static string handleRouteName(string routeName)
        {
            if (routeName.StartsWith("/"))
                return routeName;

            return $"/{routeName}";
        }
    }
}