using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/*
* ButtonScript
* 要求：挂载于Image之上，有EventTrigger
* 按钮按下后执行MenuScript.BuggtonClicked(ButtonId)
*/
public class ButtonScript : MonoBehaviour {
    public MenuScript ParentMenu;
    public int ButtinId = 0;
    public Sprite NormalImage;
    public Sprite SelectedImage;
    public Sprite DisableImage;
    public bool Disable = false;
    public bool Selected = false;
    public UnityEngine.UI.Text text;

    private UnityEngine.UI.Image Img;
    public void OnSelected(bool isSelected)
    {
        if (Disable&&!Selected && isSelected)
            return;
        Selected = isSelected;
        if(Img == null)
            Img = GetComponent<UnityEngine.UI.Image>(); 
        Img.sprite = isSelected?SelectedImage:NormalImage;
    }
    public void SetDisable(bool isDisable)
    {

        if (isDisable == Disable || DisableImage == null)
            return;
        if (Img == null)
            Img = GetComponent<UnityEngine.UI.Image>();
        Disable = isDisable;
        Img.sprite = isDisable ? DisableImage : Selected ? SelectedImage : NormalImage;
    }

    void Start()
    {
        //获取Img图片框
        Img = GetComponent<UnityEngine.UI.Image>();
        //设置监听事件
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(entry);
        if(Selected && !Disable)
        {
            Img.sprite = SelectedImage;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ParentMenu.OnButtinSelected(ButtinId);//发送消息给ParentMenu
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ParentMenu.OnButtinClicked(ButtinId);
    }
    public void SetText(string _text)
    {
        text.text = _text;
    }
}
