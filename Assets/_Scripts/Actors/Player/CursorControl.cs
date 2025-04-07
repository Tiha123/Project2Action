using UnityEngine;
using UnityEngine.InputSystem;
using CustomInspector;
using System.Collections.Generic;


public enum CursorType { Move = 0, Interact, Attack, Dialogue }


[System.Serializable]
public class CursorData
{
    public CursorType type;
    public Texture2D cursorAttatchTexture;
    public Vector2 cursorOffset;
}

public class CursorControl : MonoBehaviour
{
    public bool isShow = false;

    [ReadOnly] public Transform eyePoint;
    [ReadOnly] public Transform cursorFixedPoint;
    [SerializeField] Transform cursorP;
    [SerializeField] Transform cursorFixedP;
    [SerializeField] LineRenderer line;
    private Camera cam;
    public Transform EyePoint { get => eyePoint; set => eyePoint = value; }
    public Transform CursorFixedPoint { get => cursorFixedP; }

    public CursorType cursorType = CursorType.Move;

    [SerializeField] List<CursorData> cursors = new List<CursorData>();

    private CursorSelectable currHovered;

    private CursorSelectable prevHovered;

    void Start()
    {
        cam = Camera.main;
        if (TryGetComponent(out line) == false)
        {
            Debug.LogWarning("CursorContorl ] LineRenderer 없음");
        }
        line.enabled = isShow;
        cursorP.GetComponent<MeshRenderer>().enabled = isShow;
        cursorFixedP.GetComponent<MeshRenderer>().enabled = isShow;
        SetCursor(cursorType);
    }

    void Update()
    {
        if (cam == null)
        {
            return;
        }
        prevHovered = currHovered;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            currHovered = hit.collider.gameObject.GetComponent<CursorSelectable>();
            if (currHovered != prevHovered)
            {
                OnHoverEnter();
            }

            cursorP.position = hit.point;
            cursorFixedP.position = new Vector3(hit.point.x, eyePoint.position.y, hit.point.z);

            DrawLine();
        }
        else
        {
            if (prevHovered != null)
            {
                OnHoverExit(currHovered);
            }
            currHovered = null;

        }
    }

    void DrawLine()
    {
        line.SetPosition(0, cursorP.position);
        line.SetPosition(1, cursorFixedP.position);
    }

    public void SetCursor(CursorType type)
    {
        var cursor = cursors.Find(x => x.type == type);
        if (cursor == null)
        {
            Debug.LogWarning("CursorControl ] cursor 없음");
        }
        else
        {
            Cursor.SetCursor(cursor.cursorAttatchTexture, cursor.cursorOffset, CursorMode.Auto);
        }
    }

    void OnHoverEnter()
    {
        if (prevHovered != null)
        {
            //prevHovered.layer=LayerMask.NameToLayer("Default");
            prevHovered.Select(false);
            SetCursor(CursorType.Move);
        }
        if (currHovered == null)
        {
            return;
        }

        var sel = currHovered.GetComponentInParent<CursorSelectable>();
        if (sel == null)
        {
            return;
        }

        //currHovered.layer=LayerMask.NameToLayer("Outline");
        currHovered.Select(true);
        SetCursor(sel.cursorType);

    }

    void OnHoverExit(CursorSelectable o)
    {
        SetCursor(CursorType.Move);
        if (prevHovered != null)
        {
            //prevHovered.layer=LayerMask.NameToLayer("Default");
            prevHovered.Select(false);
        }
    }

}