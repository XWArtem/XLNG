using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class VendorGreyBackFade : MonoBehaviour
{
    private float startAlpha = 0f;
    private Image GreyBackground;
    private Color tempColor;

    void Start()
    {
        GreyBackground = GetComponent<Image>();
        tempColor = GreyBackground.color;
        tempColor.a = startAlpha;
        StartCoroutine("VisibleBackgroundOn");
    }


    IEnumerator VisibleBackgroundOn()
    {
        for (float f = 0f; f < 0.7f; f += 0.001f)
        {
            tempColor.a = f;
            GreyBackground.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
    }

}
