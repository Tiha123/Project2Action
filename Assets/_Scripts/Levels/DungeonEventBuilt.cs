using DungeonArchitect;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonEventBuilt : DungeonEventListener
{
    public bool isAuto = false;
    public NavMeshSurface nvSurface;

    private void OnValidate()
    {
        if (isAuto == false)
        {
            return;
        }
        if(nvSurface==null)
        {
            nvSurface=FindFirstObjectByType<NavMeshSurface>();
        }
        if(nvSurface==null)
        {
            nvSurface=new GameObject("NavMeshSurface").AddComponent<NavMeshSurface>();
        }
    }

    public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        base.OnPostDungeonBuild(dungeon, model);
        nvSurface?.BuildNavMesh();
    }

}
