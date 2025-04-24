using TransitionsPlus;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField] TransitionAnimator trans;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("진입");

        if(other.tag=="Player")
        {
            trans.gameObject.SetActive(true);
            CharacterControl a=other.GetComponentInParent<CharacterControl>();
            a.gameObject.SetActive(false);
        }
    }
}

