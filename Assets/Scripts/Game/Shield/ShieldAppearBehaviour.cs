using System.Collections;
using UnityEngine;

public class ShieldAppearBehaviour : MonoBehaviour
{
    private SpriteRenderer sr;
    private const float maxSize = 2F, minSize = 0;
    public bool shieldButtonReleased;
    public static ShieldAppearBehaviour Instance { get; private set; }

    private void OnEnable()
    {
        shieldButtonReleased = false;
        sr = GetComponent<SpriteRenderer>();
        var opacityAdjustment = sr.color;
        opacityAdjustment.a = 0.3F;
        sr.color = opacityAdjustment;
        StartCoroutine(ScaleShieldSprite(true));
    }

    public IEnumerator ScaleShieldSprite(bool isAppearAnimation)
    {
        float timer = 0;

        switch (isAppearAnimation)
        {
            case true:
                while (true)
                {
                    while (transform.localScale.x < maxSize)
                    {
                        timer += Time.deltaTime;
                        transform.localScale += new Vector3(0.3F, 0.3F, 1) * Time.deltaTime * 26;
                        yield return null;
                    }
                    yield return new WaitForSeconds(0);
                }

            case false:
                while (true)
                {
                    while (transform.localScale.x > minSize)
                    {
                        timer += Time.deltaTime;
                        transform.localScale -= new Vector3(0.1F, 0.1F, 1) * Time.deltaTime * 26;
                        yield return null;
                    }
                    gameObject.SetActive(false);
                    yield return new WaitForSeconds(0);
                }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (shieldButtonReleased)
        {
            StartCoroutine(ScaleShieldSprite(false));
        }
    }
}