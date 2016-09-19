using UnityEngine;
using System.Collections;
/*
 * 纵向按钮菜单，支持上下键，返回按钮为null则不创建
 */
public class ButtonsMenu : MenuScript
{
    public AudioSource ClickAudio;

    public ButtonScript SampleButton;
    public string[] ButtonNames;
    public ButtonScript BackButton;

    public int EscapeButton;

    protected ButtonScript[] Buttons;
    private int Selected;
    public bool KeepSelect;//重加载Menu的时候是否保留之前的位置？

    private bool firstEnableUpdate;
    private bool firstEnableFixedUpdate;
    public void OnEnable()
    {
        firstEnableFixedUpdate = firstEnableUpdate = true;
        if (!KeepSelect && Buttons != null && Buttons[Selected] != null)
        {
            Buttons[Selected].OnSelected(false);
            Buttons[Selected = 0].OnSelected(true);
        }
    }

    public void Start()
    {
        Selected = 0;
        Buttons = new ButtonScript[ButtonNames.Length + (BackButton == null ? 0 : 1)];
        for (int i = 0; i < Buttons.Length; ++i)
        {
            if (i != ButtonNames.Length)
            {
                if (ButtonNames[i] == "")
                    continue;//skip space Button
                //NormalButton
                Buttons[i] = Instantiate(SampleButton) as ButtonScript;

                RectTransform rt = Buttons[i].GetComponent<RectTransform>();
                rt.SetParent(GetComponent<Transform>(), false);
                Buttons[i].ParentMenu = this;

                Vector2 v2 = new Vector2(0.5f, (Buttons.Length - i + .5f) / (Buttons.Length + 2));
                rt.anchorMin = v2;
                v2 = new Vector2(0.5f, (Buttons.Length - i + .5f) / (Buttons.Length + 2));
                rt.anchorMax = v2;
                Buttons[i].ButtinId = i;
                Buttons[i].SetText(ButtonNames[i]);
            }
            else
            {
                //BackButton
                Buttons[i] = Instantiate(BackButton) as ButtonScript;
                RectTransform rt = Buttons[i].GetComponent<RectTransform>();
                rt.SetParent(GetComponent<Transform>(), false);
                Buttons[i].ParentMenu = this;

                Vector2 v2 = new Vector2(0.5f, (Buttons.Length - i + .5f) / (Buttons.Length + 2));
                rt.anchorMin = v2;
                v2 = new Vector2(0.5f, (Buttons.Length - i + .5f) / (Buttons.Length + 2));
                rt.anchorMax = v2;
                Buttons[i].ButtinId = i;
                //Buttons[i].SetText(ButtonNames[i]);
            }
        }
        //Select the first enabled button
        while (Buttons[Selected] == null || Buttons[Selected].Disable == true)
        {
            Selected = (Selected + 1) % Buttons.Length;
        }
        Buttons[Selected].OnSelected(true);
    }
    private static bool CanBeginA;
    private static bool CanBeginB;
    public void FixedUpdate()
    {
        if (firstEnableFixedUpdate)
        {
            firstEnableFixedUpdate = false;
            return;
            //跳过第一次update；
        }
        //处理手柄
        if (JoyListener.GetDownDown || JoyListener.HGetDownDown)
        {
            Buttons[Selected].OnSelected(false);
            do
            {
                Selected = (Selected + 1) % Buttons.Length;
            } while (Buttons[Selected] == null || Buttons[Selected].Disable == true);
            Buttons[Selected].OnSelected(true);
        }
        if (JoyListener.GetUpDown || JoyListener.HGetUpDown)
        {
            Buttons[Selected].OnSelected(false);
            do
            {
                Selected = (Selected + Buttons.Length - 1) % Buttons.Length;
            } while (Buttons[Selected] == null || Buttons[Selected].Disable == true);
            Buttons[Selected].OnSelected(true);
        }
        //if (CanBeginA && Input.GetButtonDown("A"))
        //{
        //    CanBeginA = false;
        //    OnButtinClicked(Selected);
        //}
        //else
        //    CanBeginA = true;
        //if (CanBeginB && Input.GetButtonDown("B"))
        //{
        //    CanBeginB = false;
        //    OnButtinClicked(EscapeButton);
        //}
        //else
        //{
        //    CanBeginB = true;
        //}
    }

    public void Update()
    {
        if (firstEnableUpdate)
        {
            firstEnableUpdate = false;
            return;
            //跳过第一次update；
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Buttons[Selected].OnSelected(false);
            do
            {
                Selected = (Selected + 1) % Buttons.Length;
            } while (Buttons[Selected] == null || Buttons[Selected].Disable == true);
            Buttons[Selected].OnSelected(true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Buttons[Selected].OnSelected(false);
            do
            {
                Selected = (Selected + Buttons.Length - 1) % Buttons.Length;
            } while (Buttons[Selected] == null || Buttons[Selected].Disable == true);
            Buttons[Selected].OnSelected(true);
        }
        if (Input.GetKeyDown(KeyCode.Return)|| Input.GetButtonDown("A"))
        {
            OnButtinClicked(Selected);
        }
        if ((Input.GetButtonDown("B")||Input.GetKeyDown(KeyCode.Escape)) && EscapeButton != -2 )
            OnButtinClicked(EscapeButton);
    }

    public override void OnButtinSelected(int ButtinId)
    {
        if (Buttons[ButtinId].Disable)
            return;
        Buttons[Selected].OnSelected(false);
        Buttons[Selected = ButtinId].OnSelected(true);
    }
    public override void OnButtinClicked(int ButtinId)
    {
        if (ClickAudio != null)
        {
            if (ClickAudio.isPlaying)
                ClickAudio.Stop();
            ClickAudio.Play();
        }
    }
}
