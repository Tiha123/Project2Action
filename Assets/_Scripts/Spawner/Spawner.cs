using CustomInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region EVENTS
    [SerializeField] protected ActorProfile actorProfile;
    #endregion
    [HorizontalLine(color: FixedColor.Blue), HideField] public bool _l1;

    [Space(20)]
    public Transform spawnPoint;
    public float radius = 2f;
    public float lineLength = 4f;

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position, radius);
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawLine(transform.position, transform.position + transform.forward * lineLength);
    // }
}
