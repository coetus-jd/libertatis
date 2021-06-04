using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLoadScene : MonoBehaviour
{
    public string SceneName;
    public bool m_UseTouch;
    public float m_TimeDelay = 0;

    public AudioSource m_Clip;

    public void Update()
    {
        if (m_UseTouch)
        {
            if (Input.anyKeyDown)
            {
                LoadSceneName(SceneName);
            }
        }
    }

    //https://docs.unity3d.com/ScriptReference/AudioSource.html
    //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
    public void LoadSceneName(string sceneName)
    {
        if (m_Clip)
        {
            m_Clip = GetComponent<AudioSource>();
            m_Clip.Play(0);
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}