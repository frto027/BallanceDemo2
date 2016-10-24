using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SrcKey_ButtonListener : F_Object
{
    public RectTransform upImg, downImg, leftImg, rightImg, lTern, rTern, spaceImg, pauseImg;
    private bool uLT, uRT, uPause;

    public void FixedUpdate()
    {
        if (levelManager.LevelEnded)
        {
            Destroy(gameObject);
            return;
        }

        JoyListener.L.x = imgTouched(leftImg) ? -1 : (imgTouched(rightImg) ? 1 : 0);
        JoyListener.L.y = imgTouched(upImg) ? 1 : (imgTouched(downImg) ? -1 : 0);
        JoyListener.OverView = imgTouched(spaceImg);
        if (imgTouched(lTern))
        {
            if (!uLT)
            {
                JoyListener.LeftR = true;
            }
            uLT = true;
        }
        else
            uLT = false;
        if (imgTouched(rTern))
        {
            if (!uRT)
            {
                JoyListener.RightR = true;
            }
            uRT = true;
        }
        else
            uRT = false;
        if (imgTouched(pauseImg))
        {
            if (!uPause)
            {
                levelManager.Pause(true);
            }
            uPause = true;
        }
        else
            uPause = false;


    }
    public override void Pause(bool isPause)
    {
        gameObject.SetActive(!isPause);
    }
    public bool imgTouched(RectTransform img)
    {
        bool touched = false;

        foreach (Touch tch in Input.touches)
        {
            if (img.rect.Contains(tch.position - new Vector2(img.position.x, img.position.y)))
            {
                return true;
            }
        }
        return touched;
    }

}
