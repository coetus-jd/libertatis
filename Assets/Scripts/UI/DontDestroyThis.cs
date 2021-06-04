using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyThis : MonoBehaviour
{
    private static DontDestroyThis instance = null;

    [Header("Configuration")]
    public string SceneName;

    public static DontDestroyThis Instance
    {
        get { return instance; }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == SceneName)
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}