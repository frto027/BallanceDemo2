using UnityEngine;
using System.Collections;
/// <summary>
/// 用于飞船中通关区域的检测，触碰通关
/// </summary>
public class PE_Balloon_EndGameBoxScript : MonoBehaviour
{
    public HingeJoint breakJoint;
    public GameObject Platforms;

    public GameObject SafeColider;
    private LevelManager levelManager;

    public Rigidbody[] Plattes;

    public Vector3 MoveDirect = new Vector3(-1, -1, 0);

    Transform PlatformsTrans;

    public void OnEnable()
    {
        PlatformsTrans = Platforms.GetComponent<Transform>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public void FixedUpdate()
    {
        if (levelManager.LevelEnded)
        {
            PlatformsTrans.position = (PlatformsTrans.position + PlatformsTrans.InverseTransformDirection(MoveDirect) * Time.deltaTime);
            try
            {
                F_Object.levelManager.Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            catch (System.Exception) { }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() != levelManager.Player)
            return;

        if (!levelManager.DeathBigin && levelManager.isPause)
        {
            levelManager.Pause(false);
            levelManager.PauseMenu.SetActive(false);
        }
        if (!levelManager.DeathBigin && !levelManager.LevelEnded)
        {
            breakJoint.breakForce = 0;
            //Platforms.GetComponent<ConstantForce>().force = Vector3.up * levelManager.Player.GetComponent<Rigidbody>().mass * 9.81f * 3f;
            //Platforms.GetComponent<PE_Balloon_VelocitySteady>().enabled = true;

            //ConfigurableJoint cfj = Platforms.GetComponent<ConfigurableJoint>();

            /*JointDrive jd = cfj.xDrive;
            /d.positionSpring = 0;jd.positionDamper = 0;
            cfj.xDrive = jd;

            jd = cfj.yDrive;
            jd.positionSpring = 0; jd.positionDamper = 0;
            cfj.yDrive = jd;*/
            Platforms.GetComponent<Rigidbody>().isKinematic = true;

            foreach (Rigidbody pt in Plattes)
            {
                pt.useGravity = true;
            }
            if (SafeColider != null)
                SafeColider.SetActive(true);
            levelManager.LevelEnd();
        }
    }
}
