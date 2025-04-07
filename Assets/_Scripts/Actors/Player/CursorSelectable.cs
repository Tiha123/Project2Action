using Unity.VisualScripting;
using UnityEngine;

public class CursorSelectable : MonoBehaviour
{
    public CursorType cursorType;
    public MeshRenderer meshRenderer;
    public void Select(bool on)
    {
        if (meshRenderer == null)
        {
            return;
        }
        string layerName = on ? "Outline" : "Default";
        if (on)
        {
            meshRenderer.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
        else
        {
            meshRenderer.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

}
