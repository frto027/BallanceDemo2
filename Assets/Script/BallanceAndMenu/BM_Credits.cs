using UnityEngine;
using System.Collections;

public class BM_Credits : MenuScript
{
    public AudioSource ClickedAudio;
    public ButtonScript BackButton;
    public RectTransform ScroolViewContext;
    // Use this for initialization
    void Start()
    {
        BackButton.ParentMenu = this;
        BackButton.OnSelected(true);
        BackButton.ButtinId = 0;
    }

    public override void OnButtinClicked(int ButtinId)
    {
        if(ClickedAudio != null)
        {
            if (ClickedAudio.isPlaying)
                ClickedAudio.Stop();
            ClickedAudio.Play();
        }
        BackToParent();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)|| Input.GetButtonDown("B")|| Input.GetButtonDown("A"))
            OnButtinClicked(0);
        Vector3 v3 = ScroolViewContext.position;
        v3.y -= JoyListener.R.y * 10;
        ScroolViewContext.position = v3;
    }
}
