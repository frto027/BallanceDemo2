using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GravityControl : F_Object
{
    public RectTransform pause, overview, leftT, rightT;
    private bool lp, ll, lr;

    public float GravityMax, GravityMin;

    public override void Start()
    {
        base.Start();
        lp = ll = lr = false;
    }
    public void FixedUpdate()
    {
        if (levelManager.LevelEnded)
        {
            Destroy(gameObject);
            return;
        }
        if (Input.acceleration.y > 0)
        {
            JoyListener.L.y = Input.acceleration.y > GravityMax ? 1 : (Input.acceleration.y < GravityMin ? 0 : (Input.acceleration.y - GravityMin) / (GravityMax - GravityMin));
        }else
        {
            JoyListener.L.y = Input.acceleration.y < -GravityMax ? -1 : (Input.acceleration.y > -GravityMin ? 0 : (Input.acceleration.y + GravityMin) / (GravityMax - GravityMin));
        }
        if (Input.acceleration.x > 0)
        {
            JoyListener.L.x = Input.acceleration.x > GravityMax ? 1 : (Input.acceleration.x < GravityMin ? 0 : (Input.acceleration.x - GravityMin) / (GravityMax - GravityMin));
        }else
        {
            JoyListener.L.x = Input.acceleration.x < -GravityMax ? -1 : (Input.acceleration.x > -GravityMin ? 0 : (Input.acceleration.x + GravityMin) / (GravityMax - GravityMin));

        }

        if (!imgTouched(leftT))
            ll = false;
        if (!imgTouched(rightT))
            lr = false;
        if (!imgTouched(pause))
            lp = false;
        if(imgTouched(leftT) && !ll)
        {
            ll = JoyListener.LeftR = true;
        }
        if (imgTouched(rightT) && !lr)
        {
            lr = JoyListener.RightR = true;
        }
        if(imgTouched(pause) && !lp)
        {
            lp = true;
            levelManager.Pause(true);
        }
        JoyListener.OverView = imgTouched(overview);
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
