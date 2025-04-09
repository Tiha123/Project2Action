using UnityEngine;

public class CursorSelectable : MonoBehaviour
{
    public CursorType cursorType;
    public Renderer targetRenderer;
    
    [Tooltip("아웃라인 Material")]
    public Material selectableMaterial;
    [Tooltip("아웃라인 두께")]
    public float selectableThickness=0.05f;

    public void SetupRenderer()
    {
        if(targetRenderer!=null)
        {
            return;
        }
        Debug.Log("못찾음1");
        targetRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if(targetRenderer != null)
        {
            return;
        }
        Debug.Log("못찾음2");
        targetRenderer=GetComponentInChildren<MeshRenderer>();
    }

    public void Select(bool on)
    {
        if (targetRenderer == null)
        {
            return;
        }
        string layerName = on ? "Outline" : "Default";
        targetRenderer.gameObject.layer = LayerMask.NameToLayer(layerName);

        selectableMaterial?.SetFloat("_Thickness", selectableThickness);
    }

}
