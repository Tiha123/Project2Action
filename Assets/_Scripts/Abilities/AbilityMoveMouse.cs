using UnityEngine;
using UnityEngine.AI;

public class AbilityMoveMouse : Ability<AbilityMoveMouseData>
{

    private Camera camera;
    private NavMeshPath path;
    private Vector3[] corners;

    public AbilityMoveMouse(AbilityMoveMouseData data, CharacterControl ow) : base(data, ow)
    {
        camera=Camera.main;
        path=new NavMeshPath();
    }

    public override void Update()
    {
        if(owner==null||owner.cc==null)
        {
            return;
        }
        InputMouse();
    }

    void InputMouse()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hit))
            {
               SetDestination(hit.point);
            }
            DrawDebugPath();
        }
    }

    void SetDestination(Vector3 destination)
    {
        if(!NavMesh.CalculatePath(owner.transform.position, destination, NavMesh.AllAreas,path))
        {
            return;
        }
        corners=path.corners;
        
    }

    private void DrawDebugPath()
    {
        if(corners == null)
        {
            return;
        }
        for (int i=0;i<corners.Length-1;++i)
        {
            Debug.DrawLine(corners[i],corners[i+1],Color.blue,0.4f);
        }
    }
}
