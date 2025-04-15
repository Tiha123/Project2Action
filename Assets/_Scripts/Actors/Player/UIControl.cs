using CustomInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [ReadOnly] public Transform uiRoot;

//임시
    [SerializeField] TextMeshPro textmesh;
    [SerializeField] Slider sliderHealth;
//임시

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

    public void SetHealth(int max, int current)
    {
        if(sliderHealth==null)
        {
            return;
        }
        float val = (float)current;
        float val2 = (float)max;
        float val3=current/max;

        sliderHealth.value=val3;
    }
}
