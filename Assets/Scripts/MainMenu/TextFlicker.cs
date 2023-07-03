using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFlicker : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pushAnyKeyText;
    // Start is called before the first frame update

    private void Awake()
    {
        StartCoroutine(FlickerText());
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator FlickerText()
    {
        while (true)
        {
            if (pushAnyKeyText.gameObject.activeInHierarchy)
            {
                pushAnyKeyText.gameObject.SetActive(false);
            }
            else
            {
                pushAnyKeyText.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.7F);
        }
    }
}
