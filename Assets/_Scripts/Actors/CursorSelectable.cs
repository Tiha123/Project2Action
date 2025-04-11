using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorSelectable : MonoBehaviour
{
    public CursorType cursorType;
    public Renderer[] meshRenderers;
    
    [Tooltip("아웃라인 Material")]
    public Material selectableMaterial;
    [Tooltip("아웃라인 두께")]
    public float selectableThickness=0.05f;

    public void SetupRenderer()
    {
        if(meshRenderers.Length>0)
        {
            return;
        }
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        if(meshRenderers.Length>0)
        {
            return;
        }
        meshRenderers=GetComponentsInChildren<MeshRenderer>();
    }

    public void Select(bool on)
    {
        if (meshRenderers == null || meshRenderers.Length<=0)
        {
            return;
        }
        string layerName = on ? "Outline" : "Default";
        foreach(Renderer m in meshRenderers)
        {
            m.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
        selectableMaterial?.SetFloat("_Thickness", selectableThickness);

    }

}
