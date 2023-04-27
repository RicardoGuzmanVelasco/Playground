using System;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

    TMP_Text every;
    TMP_Text thing;
    TMP_Text @is;
    TMP_Text quiet;
    
    AudioSource audioSource;

    void Awake()
    {
        background = GameObject.Find("Background").GetComponent<Image>();
        signature = GameObject.Find("Signature").GetComponent<Image>();
        
        gameBy = GameObject.Find("GameBy").GetComponent<TMP_Text>();
        author = GameObject.Find("Me").GetComponent<TMP_Text>();
        
        every = GameObject.Find("Every").GetComponent<TMP_Text>();
        thing = GameObject.Find("Thing").GetComponent<TMP_Text>();
        @is = GameObject.Find("Is").GetComponent<TMP_Text>();
        quiet = GameObject.Find("Quiet").GetComponent<TMP_Text>();
        
        audioSource = FindObjectOfType<AudioSource>();
    }

    async void Start()
    {
        HideThingsToBeginWith();
        
        await FadeInBackground();

        await FadeInSignature();
        await SyncWithTheme();
        await FadeOutSignature();
        
        await SpawnTitle();
        
        await TheMusicToStart();
        await SlideInAuthorFromLeft();
    }

    static Task TheMusicToStart()
    {
        return Delay(FromSeconds(1.5));
    }

    async Task SyncWithTheme()
    {
        await Delay(FromSeconds(1));
        audioSource.Play();
    }

    async Task SpawnTitle()
    {
        await Delay(FromSeconds(.75f));
        Compose(every);
        await Delay(FromSeconds(.2f));
        await Compose(thing).AsyncWaitForCompletion();
        await Compose(@is).AsyncWaitForCompletion();
        await Compose(quiet).AsyncWaitForCompletion();
        
        Sequence Compose(TMP_Text text)
        {
            return Sequence()
                .AppendCallback(() => text.alpha = 1)
                .Append(text.DOFadeInCharEm(.1f).SetEase(OutExpo))
                .AppendInterval(.35f);
        }
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
        
        every.DOFade(0, 0).Complete();
        thing.DOFade(0, 0).Complete();
        @is.DOFade(0, 0).Complete();
        quiet.DOFade(0, 0).Complete();

        gameBy.rectTransform.DOAnchorPosX(-1000, 0).Complete();
        author.rectTransform.DOAnchorPosX(-1000, 0).Complete();
    }
}

static class Extensions
{
    public static Tweener DOFadeInCharEm(this TMP_Text text, float duration)
    {
        return To(() => text.characterSpacing, x => text.characterSpacing = x, 0, duration).From(500).SetEase(OutQuad);
    }   
}