using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackscreenFade : MonoBehaviour
{
    [SerializeField] private GameObject myObject;
    [SerializeField] private CanvasGroup myUIGroup;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public void FadeInAndOut()
    {
        fadeIn = true;
        myObject.SetActive(true);
    }

    public void Update()
    {
        if (fadeIn)
        {
            if (!fadeOut)
            {
                if (myUIGroup.alpha < 1)
                {
                    myUIGroup.alpha += Time.deltaTime;
                    if (myUIGroup.alpha >= 1)
                    {
                        fadeOut = true;
                    }
                }
            }
            else
            {
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha <= 0)
                {
                    fadeIn = false;
                    fadeOut = false;
                    myObject.SetActive(true);
                }
            }
        }
    }
}
