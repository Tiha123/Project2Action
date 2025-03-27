using UnityEngine;
using UnityEngine.InputSystem;

public class CursorControl : MonoBehaviour
{
    public bool isShow=false;

    [Space(10), SerializeField] Transform eyePoint;
    [SerializeField] Transform hitP;
    [SerializeField] Transform cursorP;
    [SerializeField] LineRenderer line;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        
        if (TryGetComponent(out line)==false)
        {
            Debug.LogWarning("CursorContorl ] LineRenderer 없음");
        }
        if(isShow)
        {
            line.enabled=true;
            hitP.GetComponent<MeshRenderer>().enabled=true;
            cursorP.GetComponent<MeshRenderer>().enabled=true;
        }
        else
        {
            line.enabled=false;
            hitP.GetComponent<MeshRenderer>().enabled=false;
            cursorP.GetComponent<MeshRenderer>().enabled=false;
        }
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            hitP.position = hit.point;
            cursorP.position = new Vector3(hit.point.x, eyePoint.position.y, hit.point.z);
            DrawLine();
        }
    }

    void DrawLine()
    {
        line.SetPosition(0, hitP.position);
        line.SetPosition(1, cursorP.position);
    }

}