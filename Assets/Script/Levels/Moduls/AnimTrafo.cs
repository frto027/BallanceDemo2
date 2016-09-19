using UnityEngine;
using System.Collections;

public class AnimTrafo : MonoBehaviour
{
    public NoAnimTrafo ParentTrafo;

    public GameObject GameObjToBeDestoryed;

    public Material PointColorShander;
    public Color color1, color2;

    public Material flashMaterial;
    private float flashInDelay;
    private bool flashFirst;
    public float FlashDelay;

    public void ChangePointColor(Color color1, Color color2)
    {
        PointColorShander.SetColor("_Color", color1);
        PointColorShander.SetColor("_EmissionColor", color2);
    }
    public void AnimEndCallBack()
    {
        ParentTrafo.AnimTrafoCallBack();
        Destroy(GameObjToBeDestoryed);
    }
    public void PlayAudio()
    {
        if (F_Object.levelManager.ChangeBallAudio.isPlaying)
            F_Object.levelManager.ChangeBallAudio.Stop();
        F_Object.levelManager.ChangeBallAudio.Play();
    }

    public void Update()
    {
        if (flashInDelay > FlashDelay)
            flashInDelay = FlashDelay;
        flashInDelay -= Time.deltaTime;
        if (flashInDelay <= 0)
        {
            
            //switch
            flashFirst = !flashFirst;
            flashMaterial.SetTextureOffset("_MainTex", new Vector2(flashFirst?-1.5f:-2f, 0f));

            flashInDelay = FlashDelay;
        }
        
    }
}
