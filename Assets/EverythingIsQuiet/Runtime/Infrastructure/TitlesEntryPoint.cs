using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;
using static DG.Tweening.DOTween;
using static DG.Tweening.Ease;

public class TitlesEntryPoint : MonoBehaviour
{
    Image background;
    Image signature;

    TMP_Text gameBy;
    TMP_Text author;

    TMP_Text everything;
    TMP_Text @is;
    TMP_Text quiet;
    
    AudioSource audioSource;

    void Awake()
    {
        background = GameObject.Find("Background").GetComponent<Image>();
        signature = GameObject.Find("Signature").GetComponent<Image>();
        
        gameBy = GameObject.Find("GameBy").GetComponent<TMP_Text>();
        author = GameObject.Find("Me").GetComponent<TMP_Text>();
        
        everything = GameObject.Find("Everything").GetComponent<TMP_Text>();
        @is = GameObject.Find("Is").GetComponent<TMP_Text>();
        quiet = GameObject.Find("Quiet").GetComponent<TMP_Text>();
        
        audioSource = FindObjectOfType<AudioSource>();
    }

    async void Start()
    {
        HideThingsToBeginWith();
        
        await FadeInBackground();

        await FadeInSignature();
        await Delay(FromSeconds(1));
        audioSource.Play();
        await FadeOutSignature();
        await Delay(FromSeconds(.5f));
        await SpawnTitle();

        await Delay(FromSeconds(2));
        await SlideInAuthorFromLeft();
    }

    Task SpawnTitle()
    {
        return Sequence()
            .Append(everything.DOFade(1, .5f).From(0).SetEase(InQuint))
            .Append(@is.DOFade(1, .5f).From(0).SetEase(InQuint))
            .Append(quiet.DOFade(1, .5f).From(0).SetEase(InQuint))
            .AsyncWaitForCompletion();
    }

    Task SlideInAuthorFromLeft()
    {
        return Sequence()
            .Append(gameBy.rectTransform.DOAnchorPosX(1000, .5f).SetRelative(true))
            .AppendInterval(.5f / 2)
            .Append(author.rectTransform.DOAnchorPosX(1000, .5f).SetRelative(true))
            .AsyncWaitForCompletion();
    }

    Task FadeInSignature()
    {
        return signature.DOFade(1, .5f).From(0).SetEase(OutQuad).AsyncWaitForCompletion();
    }
    
    Task FadeOutSignature()
    {
        return signature.DOFade(0, .5f).SetEase(InQuad).AsyncWaitForCompletion();
    }

    Task FadeInBackground()
    {
        return background.DOFade(1, 1f).From(0).SetEase(InOutSine).AsyncWaitForCompletion();
    }

    void HideThingsToBeginWith()
    {
        signature.DOFade(0, 0).Complete();
        
        everything.DOFade(0, 0).Complete();
        @is.DOFade(0, 0).Complete();
        quiet.DOFade(0, 0).Complete();

        gameBy.rectTransform.DOAnchorPosX(-1000, 0).Complete();
        author.rectTransform.DOAnchorPosX(-1000, 0).Complete();
    }
}