using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutEffect : MonoBehaviour
{
    public Image blackoutImage;
    public string fadeType;
    public bool isFadeTime;
    public static FadeInOutEffect Instance { get; private set; }
    // Start is called before the first frame update

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (isFadeTime)
        {
            if (fadeType != null)
            {
                if (fadeType == "in")
                {
                    blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, blackoutImage.color.a + 0.1F);
                    StartCoroutine(ExitFading());
                }
                else if (fadeType == "out")
                {
                    blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, blackoutImage.color.a - 0.1F);
                    StartCoroutine(ExitFading());
                }
            }
        }
    }

    private IEnumerator ExitFading()
    {
        yield return new WaitForSeconds(1.6F);
        isFadeTime = false;
    }
}