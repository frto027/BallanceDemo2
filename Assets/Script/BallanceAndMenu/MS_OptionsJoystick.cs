using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MS_OptionsJoystick : MenuScript
{
    public AudioSource ClickAudio;
    public ButtonScript BackButton;

    public Text Up;
    public Text Down;
    public Text Left;
    public Text Right;
    public Text LB;
    public Text RB;
    public Text B;
    public Text A;
    public Text X;
    public Text Y;
    public Text Select;
    public Text StarTT;

    public RectTransform LT;
    public RectTransform RT;
    public RectTransform L;
    public RectTransform R;

    private Text LText,RText;

    //private Vector3 initPosL;
    //private Vector3 initPosR;

    public void Start()
    {
        //initPosL = L.position;
        //initPosR = R.position;
        LText = L.GetComponent<Text>();
        RText = R.GetComponent<Text>();
    }

    public void OnEnable()
    {

        BackButton.ButtinId = 0;
        BackButton.OnSelected(true);
    }

    public override void OnButtinClicked(int ButtinId)
    {
        if (ClickAudio.isPlaying)
            ClickAudio.Stop();
        ClickAudio.Play();
        switch (ButtinId)
        {
            case 0:
                BackToParent();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Up.color = Input.GetAxis("UpDown") > 0 ? Color.red : Color.white;
        Down.color = Input.GetAxis("UpDown") < 0 ? Color.red : Color.white;
        Right.color = Input.GetAxis("LeftRight") > 0 ? Color.red : Color.white;
        Left.color = Input.GetAxis("LeftRight") < 0 ? Color.red : Color.white;
        LB.color = Input.GetButton("LB") ? Color.red : Color.white;
        RB.color = Input.GetButton("RB") ? Color.red : Color.white;

        A.color = Input.GetButton("A") ? Color.red : Color.white;
        B.color = Input.GetButton("B") ? Color.red : Color.white;
        X.color = Input.GetButton("X") ? Color.red : Color.white;
        Y.color = Input.GetButton("Y") ? Color.red : Color.white;

        LText.color = Input.GetButton("L") ? Color.red : Color.white;
        RText.color = Input.GetButton("R") ? Color.red : Color.white;

        Select.color = Input.GetButton("Select") ? Color.red : Color.white;
        StarTT.color = Input.GetButton("Start") ? Color.red : Color.white;


        Vector2 v2 = LT.sizeDelta;
        v2.y = 100 * Input.GetAxis("LTrigger");
        LT.sizeDelta = v2;
        v2 = RT.sizeDelta;
        v2.y = 100 * Input.GetAxis("RTrigger");
        RT.sizeDelta = v2;

        L.localPosition = /*initPosL + */(Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up)*20;
        R.localPosition = /*initPosR + */(Input.GetAxis("RHorizontal") * Vector3.right + Input.GetAxis("RVertical") * Vector3.up)*20;
        if(Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Return))
        {
            OnButtinClicked(0);
        }
    }
}
