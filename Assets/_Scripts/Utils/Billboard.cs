using System.Collections;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform maincam;
    [SerializeField] Transform offset;
    public float offsetForce=0.5f;
    IEnumerator Start()
    {   
        yield return new WaitUntil(()=>Camera.main!=null);
        maincam=Camera.main.transform;
    }

    void Update()
    {
        if(maincam==null)
        {
            return;
        }

        Vector3 euler=(maincam.position-transform.position).normalized;

        transform.rotation=Quaternion.LookRotation(-euler, maincam.up);
        offset.position=transform.position-maincam.forward*offsetForce;
    }

    public void Visible(bool on)
    {
        gameObject.SetActive(on);
    }
}
