using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonUI : MonoBehaviour
{
    private Selectable m_Selectable;
    public string m_Key = "level";
    public bool m_UseDelete = true;

    private void Awake()
    {
        m_Selectable = GetComponent<Selectable>();
    }

    private void OnEnable()
    {
        var hasKey = PlayerPrefs.HasKey(m_Key);
        m_Selectable.interactable = PlayerPrefs.HasKey(m_Key);

        if (!hasKey && m_UseDelete)
            Destroy(gameObject);
    }
}
