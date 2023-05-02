using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List <Cinemachine.CinemachineVirtualCamera> cinemachineCameras;
    // Start is called before the first frame update

    #region Delegate
    public delegate void addCameraFollow(GameObject player);
    public static addCameraFollow AddCameraFollow;
    #endregion

    private void Awake()
    {
        LandingAndLifting.PlayerBaseAnim += ChangeCamera;
        AddCameraFollow += AddLiftingCameraFolowObject;
    }



    private void AddLiftingCameraFolowObject(GameObject player)
    {
        cinemachineCameras[1].Follow = player.transform;
    }
    private void ChangeCamera(int LandingOrLifting)
    {
        for (int i = 0; i < cinemachineCameras.Count; i++)
        {
            if (i == LandingOrLifting)
            {
                CameraActive(i, true);
            }
            else
            {
                CameraActive(i,false );
            }
        }
    }
    private void CameraActive(int number,bool isActive)
    {
        cinemachineCameras[number].gameObject.SetActive(isActive);
    }
    private void OnDestroy()
    {
        LandingAndLifting.PlayerBaseAnim -= ChangeCamera;
        AddCameraFollow -= AddLiftingCameraFolowObject;
    }
}
