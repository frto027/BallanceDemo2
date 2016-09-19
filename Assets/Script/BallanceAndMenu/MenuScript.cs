using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
    public GameObject ParentMenu;
    public virtual void OnButtinSelected(int ButtinId)
    {
        Debug.Log("On Button Selected " + ButtinId.ToString());
    }
    public virtual void OnButtinClicked(int ButtinId)
    {
        Debug.Log("On Button Clicked " + ButtinId.ToString());
    }
    public void BackToParent()
    {
        if (gameObject.activeSelf == true && ParentMenu != null)
        {
            gameObject.SetActive(false);
            ParentMenu.SetActive(true);
        }
    }
    public void GoToMenu(GameObject Menu)
    {
        if (gameObject.activeSelf == true && Menu != null)
        {
            MenuScript ms = Menu.GetComponent<MenuScript>();
            if (ms != null)
                ms.ParentMenu = gameObject;
            gameObject.SetActive(false);
            Menu.SetActive(true);
        }
    }
}

