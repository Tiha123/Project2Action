using CustomInspector;
using MoreMountains.Feedbacks;
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

    void Awake()
    {
        uiRoot=transform.Find("_UI_");
        if(uiRoot==null)
        {
            Debug.LogWarning("UIControl ] _UI_없음");
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
        else
        {
            uiRoot.gameObject.SetActive(true);
        }
    }
    public void Display(string info)
    {
        if(textmesh==null)
        {
            return;
        }
        textmesh.text=info;
    }

    public void SetHealth(int max, int current)
    {
        if(sliderHealth==null)
        {
            return;
        }
        float val3=current/max;

        sliderHealth.value=val3;
    }
}
