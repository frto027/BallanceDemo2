using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int ExtraLife = 3;
    public float Score = 1000f;

    public string [] JoystickResPath;//移动端控制插件的prefabs路径，根据类型动态加载
    public Vector3 RebirthPosition;//死亡后重生位置

    public Transform CameraAim;
    public Transform CameraDirection;//决定了球的朝向

    private Animator CameraDirectionAnimator;//动画，用于控制旋转

    public F_Player Player;

    public F_Camera theCamera;
    private Transform theCameraT;

    public GameObject GameUI;
    public GameObject PauseMenu;
    public GameObject DeathMenu;

    public JoyListener joyListener;

    public bool isPause = false;//read only,please use Pause(bool)

    public int deviceType;

    public bool DeathBigin = false;//read only,please use Death()

    public bool birthing = false;//请birthball来更改，brithing无分数降低，无法开启菜单

    public bool changingBall = false;//是否正在变球

    public ScoreSaver scoreSaver;
    private bool DeathAfter = false;

    public Animator DeathAnimi;

    public AudioSource RebirthAudio;

    public bool LevelEnded = false;
    public GameObject LevelEndUI;

    public int CurrentLevel;//readonly 0 - 12

    public int CurrentSector;//First is 1

    public bool DebugBool = false;
    //几个公用音频资源
    public AudioSource BallScrollAudio_Floor;//滚球声音
    public AudioSource BallScrollAudio_Wood;//滚球声音
    public AudioSource BallScrollAudio_Rail;//滚球声音
    public AudioSource BallCrashAudio;//击球声音
    public AudioSource WoodFlapAudio;//木板拍击

    public AudioSource ChangeBallAudio;//变球器声音
    public AudioSource BackGroundMusicAudio;//BGM

   

    public bool ShowGameUI = true;
    public void LevelEnd()
    {
        //通关了
        if (LevelEnded || DeathBigin)
            return;
        if (isPause)
            Pause(false);
        LevelEnded = true;
        //结束关卡
        JoyListener.L = Vector2.zero;
        LevelEndUI.SetActive(true);
        theCamera.CameraPositionS1 = 0.02f;
        theCamera.CameraPositionS2 = 0.01f;
        theCamera.CameraPositionS3 = 0.02f;
    }


    /*死亡相关*/
    public void Death()
    {
        if (DeathBigin || birthing)
            return;
        DeathBigin = true;
        DeathAfter = false;

        if (ExtraLife <= 0)
        {
            theCamera.CameraPositionS1 = theCamera.CameraPositionS2 = theCamera.CameraPositionS3 = 0;
            DeathMenu.SetActive(true);
            if (RebirthAudio.isPlaying)
                RebirthAudio.Stop();
            RebirthAudio.Play();
            return;
        }
        ExtraLife--;
        DeathAnimi.SetBool("Deathing", true);

        //此处死亡动画，白色笼罩，回调DeathReset
    }
    public void DeathReset()
    {
        //白色已卡住
        DeathAfter = true;
        F_Object.ResetAll();
        if (F_Object.HasReseting())
            return;
        AfterDeathReset();
    }
    private void AfterDeathReset()
    {

        //此处撤除白色动画
        DeathAfter = false;
        DeathAnimi.SetBool("Deathing", false);

        //动画回调OverDeath
    }
    public void OverDeath()
    {
        //白色彻底撤除
        DeathBigin = false;
    }
    /* over 死亡相关*/

    /*暂停相关*/
    public void Pause(bool _isPause)
    {
        if (DeathBigin || changingBall)
            return;
        if (isPause == _isPause)
            return;
        isPause = _isPause;
        F_Object.PauseAll(isPause);
        //从false到true，显示菜单
        if (isPause)
            PauseMenu.SetActive(true);
    }
    /* over 暂停相关*/
    /*虚拟键盘接口相关*/
    /*
     * 在JoystickResPath中注册你的虚拟键盘插件路径，数组下标即菜单对应的buttonID，确保能够在游戏开始时正常加载
     * 注意，虚拟键盘也应该是F_Object
     * 在收到Pause的消息时需要隐藏
     * 在收到Pause false消息时显示
     * 当虚拟键盘开启的时候，可以操纵JoyListener类来实现角色控制
     * 如果是虚拟键盘，JoyListener实例的检测功能将在LevelManager中被销毁，
     * JoyListener中
     * L 方向键，是个二维向量，其中x控制球相对世界坐标x方向上的力
     * 旋转视角请在Update中将LeftR,RightR单帧置为1，LevelManager将在--LateUpdate-->fixedUpdate中处理
     * 视角抬高，请将JoyListener中的OverView置为1
     * 你可以制作你自己的虚拟键盘，包括对球施加力度的大小都是可控制的，输入方式也不一定是屏幕输入
    /* over 虚拟键盘相关*/

    // Use this for initialization
    void Start()
    {
        DeathBigin = false;
        //初始化CurrentLevel
        CurrentLevel = PlayerPrefs.GetInt("LoadLevelINT", 1) - 1;
        //若不是手柄，则禁用手柄监听器
        if (((deviceType = (PlayerPrefs.GetInt("DeviceType"))) != (int)DeviceType.Type.Joystick) && joyListener != null)
        {
            joyListener.Start();//初始化静态变量
            Destroy(joyListener);
        }
        scoreSaver = new ScoreSaver(Application.persistentDataPath + "/gamesave");
        if (CameraAim == null)
            CameraAim = Player.GetComponent<Transform>();
        CameraDirectionAnimator = CameraDirection.GetComponent<Animator>();
        theCameraT = theCamera.GetComponent<Transform>();

        //显示DeathAnim
        DeathAnimi.gameObject.SetActive(true);
        if (ShowGameUI)
            GameUI.SetActive(true);

        //加载移动端控制插件
        if (deviceType == (int)DeviceType.Type.TouchScreen)
        {
            int JoyType = PlayerPrefs.GetInt("TouchKeyboardId", 0);
            if(JoystickResPath.Length > JoyType)
            {
                Instantiate(Resources.Load(JoystickResPath[JoyType]));
            }
        }
    }

    public void LateUpdate()
    {
        
    }

    public void FixedUpdate()
    {
        /*debug only*/
        if (DebugBool)
        {
            DebugBool = false;
            NextSector();
        }

        /**from late update****/
        if (!isPause /* && !birthing*/)
        {
            if (!theCamera.FreeCameraPosition && !theCamera.FreeCameraAim)
            {
                //键盘监听旋转
                if ((Input.GetKeyDown(scoreSaver.Key_Turn) && Input.GetKey(scoreSaver.Key_Left)) ||
                    (Input.GetKey(scoreSaver.Key_Turn) && Input.GetKeyDown(scoreSaver.Key_Left)))
                {
                    JoyListener.LeftR = true;
                }
                if ((Input.GetKeyDown(scoreSaver.Key_Turn) && Input.GetKey(scoreSaver.Key_Right)) ||
                    (Input.GetKey(scoreSaver.Key_Turn) && Input.GetKeyDown(scoreSaver.Key_Right)))
                {
                    JoyListener.RightR = true;
                }
                //手柄监听旋转
                if (JoyListener.LeftR || Input.GetButtonDown("LB"))
                {
                    JoyListener.LeftR = false;
                    //左转
                    CameraDirectionAnimator.ResetTrigger("L");
                    CameraDirectionAnimator.SetTrigger("R");

                }
                if (JoyListener.RightR || Input.GetButtonDown("RB"))
                {
                    JoyListener.RightR = false;
                    //右转
                    CameraDirectionAnimator.ResetTrigger("R");
                    CameraDirectionAnimator.SetTrigger("L");
                }
            }
        }

        /******/
        //死亡后执行ResetUpdate，回调AfterDeathReset
        if (DeathBigin && DeathAfter)
        {
            if (F_Object.HasReseting())
                F_Object.ResetUpdateAll();
            else
                AfterDeathReset();
        }
        //按Esc或手柄Start开启暂停菜单
        if (!isPause && !LevelEnded && !birthing && (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")))
        {
            Pause(true);
        }

        if (!isPause && !birthing && !LevelEnded)
        {
            if (Score > 0)
                Score -= 2 * Time.deltaTime;
            else
                Score = 0;
            //使用键盘
            if (deviceType == (int)DeviceType.Type.KeyBoard)
            {
                //控制方向
                JoyListener.L.y = Input.GetKey(scoreSaver.Key_Up) ? 1 : Input.GetKey(scoreSaver.Key_Down) ? -1 : 0;
                JoyListener.L.x = Input.GetKey(scoreSaver.Key_Right) ? 1 : Input.GetKey(scoreSaver.Key_Left) ? -1 : 0;
            }
            //视野控制
            //这里给LT和RT设置一个小偏移，R摇杆小幅度换视野
            Vector3 v3 = Vector3.zero;
            v3 = Vector3.left * Input.GetAxis("LTrigger") + Vector3.right * Input.GetAxis("RTrigger");
            v3.x += JoyListener.R.x * 1;
            v3 = v3 + CameraDirection.TransformDirection(Vector3.Cross(theCamera.CameraMove, Vector3.right)).normalized * JoyListener.R.y;
            theCamera.CameraSoftMove = v3;


            //手柄/键盘抬高视野
            if (deviceType == (int)DeviceType.Type.KeyBoard || deviceType == (int)DeviceType.Type.Joystick)
            {
                if (Input.GetKey(scoreSaver.Key_OverView) || Input.GetButton("A") || Input.GetButton("R"))
                    theCamera.CameraSoftMove.y += 10;//上方有对y的控制，这里不宜直接修改
            }
            //屏幕键盘抬高视野
            else if(deviceType == (int)DeviceType.Type.TouchScreen && JoyListener.OverView)
            {
                theCamera.CameraSoftMove.y += 10;
            }
        }

    }

    public void NextSector()
    {
        ++CurrentSector;
        F_Object.SwitchSectorAll();
    }

}
