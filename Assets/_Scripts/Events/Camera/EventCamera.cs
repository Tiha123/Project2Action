// using Unity.Cinemachine;
// using UnityEngine;

// public class EventCamera : MonoBehaviour
// {
//     public CinemachineVirtualCameraBase eventCamera;

    
    
//     void OnTriggerEnter(Collider other)
//     {
//         if(other.tag!="Player")
//         {
//             return;
//         }
//         var cc= other.GetComponentInParent<CharacterControl>();

//         if(cc==null)
//         {
//             Debug.LogWarning($"EventCamera ] 메인카메라 없음");
//         }
//         cc.maincamera.Priority.Value -=1;
//         eventCamera.Priority.Value +=1;
//     }

//     void OnTriggerExit(Collider other)
//     {
//         if(other.tag!="Player")
//         {
//             return;
//         }
//         var cc= other.GetComponentInParent<CharacterControl>();

//         if(cc==null)
//         {
//             Debug.LogWarning($"EventCamera ] 메인카메라 없음");
//         }
//         cc.maincamera.Priority.Value +=1;
//         eventCamera.Priority.Value -=1;
//     }
// }
