using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstSelectedUI : MonoBehaviour
{
    private EventSystem m_EventSystem;
    private Selectable m_Selectable;

    private void Awake()
    {
        m_Selectable = GetComponent<Selectable>();
    }

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;
        m_EventSystem.SetSelectedGameObject(null);
        m_EventSystem.SetSelectedGameObject(gameObject);

        m_Selectable.OnSelect(null);
    }
}
