using CustomInspector;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [ReadOnly] public Transform uiRoot;
    [SerializeField] TextMeshPro textmesh;

    void Start()
    {
        uiRoot=transform.Find("_UI");
        if(uiRoot==null)
        {
            Debug.LogWarning("UICONtrol ] _UI_없음");
        }
        Show(false);
    }

    public void Show(bool on)
    {
        if(uiRoot==null)
        {
            return;
        }
        if(on==false)
        {
            uiRoot.localScale=Vector3.zero;
            return;
        }
        uiRoot.gameObject.SetActive(on);
    }
    public void Display(string info)
    {

        if(textmesh==null)
        {
            return;
        }
    }
}
