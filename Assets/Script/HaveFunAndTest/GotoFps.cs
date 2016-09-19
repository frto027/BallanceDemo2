using UnityEngine;
using System.Collections;

public class GotoFps : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() != GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().Player)
            return;
        F_Camera fc = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().theCamera;
        fc.CameraAimS1 = fc.CameraAimS2 = fc.CameraAimS3 = fc.CameraPositionS1 = fc.CameraPositionS2 = fc.CameraPositionS3 = 1f;
        fc.CameraSoftMove = Vector3.zero;
        fc.CameraFirstSoftMove = Vector3.zero;
        fc.CameraMove.z = -0.3f;
    }
}
