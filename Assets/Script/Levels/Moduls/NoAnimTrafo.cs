using UnityEngine;
using System.Collections;

public class NoAnimTrafo : F_Object
{
    public GameObject HideObj;
    private Vector3 thisTransPos;
    public float MoveSpeed = 1f;

    public AnimTrafo AnimTrafoToBeCreated;

    public Material PointMaterial;

    public Vector3 MovePosition;
    private Transform PlayerTransForm;
    private Rigidbody PlayerRigidBody;

    private bool WaitingAnim = false;

    public F_Player PlayerToBeCreated;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() != levelManager.Player || levelManager.changingBall||PlayerToBeCreated.PlayerType == levelManager.Player.PlayerType)
            return;
        thisTransPos = GetComponent<Transform>().position;
        levelManager.changingBall = true;
        PlayerTransForm = levelManager.Player.GetComponent<Transform>();
        PlayerRigidBody = PlayerTransForm.GetComponent<Rigidbody>();
        WaitingAnim = false;
        if (PlayerRigidBody != null)
            PlayerRigidBody.isKinematic = true;
    }

    public void FixedUpdate()
    {
        if (levelManager.changingBall == true && PlayerTransForm!=null)
        {
            if ((thisTransPos + MovePosition - PlayerTransForm.position).magnitude > 0.2f)
            {
                PlayerTransForm.position = (thisTransPos + MovePosition - PlayerTransForm.position) * MoveSpeed * Time.deltaTime + PlayerTransForm.position;
            }
            else
            {
                if (!WaitingAnim)
                {
                    PlayerTransForm.position = thisTransPos + MovePosition;
                    //creat AnimTrafo
                    AnimTrafo tempAnim = Instantiate(AnimTrafoToBeCreated).GetComponent<AnimTrafo>();
                    tempAnim.GetComponent<Transform>().position = thisTransPos;
                    //change Matirial Color
                    tempAnim.ChangePointColor(PointMaterial.GetColor("_Color"), PointMaterial.GetColor("_EmissionColor"));
                    //set parent trafo
                    tempAnim.ParentTrafo = this;

                    HideObj.SetActive(false);
                    WaitingAnim = true;
                }
            }
        }
    }
    public void AnimTrafoCallBack()
    {
        HideObj.SetActive(true);
        levelManager.changingBall = false;
        WaitingAnim = false;

        //send message to old ball to creat some effects
        levelManager.Player.DestoryEffect();
        //creat new ball
        Destroy(levelManager.Player.gameObject);
        levelManager.Player = Instantiate(PlayerToBeCreated).GetComponent<F_Player>();
        levelManager.Player.GetComponent<Transform>().position = thisTransPos + MovePosition;
        levelManager.CameraAim = levelManager.Player.GetComponent<Transform>();
        
    }
}
