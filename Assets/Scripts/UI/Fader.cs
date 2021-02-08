using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Fader : MonoBehaviour
{
    public Image FadeImage;

    float fadeInitialAlpha;
    float fadeTargetAlpha;
    bool fading = false;
    public bool IsFading
    {
        get { return fading; }
    }

    const float FADE_DURATION = 0.5f;
    float fadeTimer;
    
    public static Fader Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public UnityEvent OnFadeComplete;

    public void FadeIn(UnityAction callback = null)
    {
        fading = true;
        fadeTimer = 0;
        Color c = FadeImage.color;
        fadeInitialAlpha = 1f;
        FadeImage.color = c;
        fadeTargetAlpha = 0;
        OnFadeComplete.RemoveAllListeners();
        if (callback != null)
            OnFadeComplete.AddListener(callback);
    }

    public void FadeOut(UnityAction callback = null)
    {
        fading = true;
        fadeTimer = 0;
        Color c = FadeImage.color;
        fadeInitialAlpha = 0f;
        FadeImage.color = c;
        fadeTargetAlpha = 1;
        OnFadeComplete.RemoveAllListeners();
        if (callback != null)
            OnFadeComplete.AddListener(callback);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTimer < FADE_DURATION && fading)
        {
            Color c = FadeImage.color;
            c.a = Mathf.Lerp(fadeInitialAlpha, fadeTargetAlpha, fadeTimer / FADE_DURATION);
            fadeTimer += Time.deltaTime;

            if (fadeTimer > FADE_DURATION)
            {
                fading = false;
                c.a = fadeTargetAlpha;
                OnFadeComplete?.Invoke();
            }
            FadeImage.color = c;
        }        
    }
}
