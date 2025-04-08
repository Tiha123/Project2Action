using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform maincam;
    void Start()
    {
        maincam=Camera.main.transform;
    }

    void Update()
    {
        Vector3 euler=(maincam.position-transform.position).normalized;

        transform.rotation=Quaternion.LookRotation(-euler, maincam.up);
    }
}
