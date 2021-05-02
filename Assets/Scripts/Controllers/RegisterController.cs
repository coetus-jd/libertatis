using System.Collections;
using System.Text;
using PirateCave.Enums;
using PirateCave.Models;
using PirateCave.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    [SerializeField]
    private GameObject message;

    [SerializeField]
    private Sprite success;

    [SerializeField]
    private Sprite error;

    [SerializeField]
    private GameObject nick;

    [SerializeField]
    private GameObject name;

    void Start()
    {
        // StartCoroutine(register());
    }

    public void call()
    {
        StartCoroutine(register());
    }

    public IEnumerator register()
    {
        UnityWebRequest request = new UnityWebRequest($"{Helpers.apiUrl}/players", HttpMethods.POST);

        Player player = new Player()
        {
            nick = nick.GetComponent<TMP_InputField>().text,
            name = name.GetComponent<TMP_InputField>().text
        };

        string jsonFields = JsonUtility.ToJson(player);

        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonFields));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();
        
        message.SetActive(true);

        Response apiReponse = JsonUtility.FromJson<Response>(request.downloadHandler?.text);

        if (request.isNetworkError || request.isHttpError)
        {
            message.GetComponentInChildren<Image>().sprite = error;
            message.GetComponent<TMP_InputField>().text = apiReponse.data.error;
        }
        else
        {
            message.GetComponentInChildren<Image>().sprite = success;
            message.GetComponent<TMP_InputField>().text = apiReponse.data.message;
        }
    }
}
