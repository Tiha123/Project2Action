using UnityEngine;

public class CursorSelectable : MonoBehaviour
{
    public CursorType cursorType;
    public Renderer meshRenderer;
    
    [Tooltip("아웃라인 Material")]
    public Material selectableMaterial;
    [Tooltip("아웃라인 두께")]
    public float selectableThickness=0.05f;

    public void SetupRenderer()
    {
        if(meshRenderer!=null)
        {
            return;
        }
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if(meshRenderer != null)
        {
            return;
        }
        meshRenderer=GetComponentInChildren<MeshRenderer>();
    }

    public void Select(bool on)
    {
        if (meshRenderer == null)
        {
            return;
        }
        string layerName = on ? "Outline" : "Default";
        meshRenderer.gameObject.layer = LayerMask.NameToLayer(layerName);

        selectableMaterial?.SetFloat("_Thickness", selectableThickness);
    }

}
