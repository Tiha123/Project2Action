using UnityEngine;
using UnityEngine.InputSystem;
using CustomInspector;
using System.Collections.Generic;


public enum CursorType { Move, Attack, Interact, Dialogue}


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

    public CursorType cursorType;

    [SerializeField] List<CursorData> cursors = new List<CursorData>();
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
        if (cursorFixedPoint == null || cam == null)
        {
            return;
        }
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            cursorP.position = hit.point;
            cursorFixedP.position = new Vector3(hit.point.x, eyePoint.position.y, hit.point.z);
            DrawLine();
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
}