using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;
using static DG.Tweening.DOTween;
using static DG.Tweening.Ease;

namespace EverythingIsQuiet.Infrastructure
{
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

        Button @continue;

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

            @continue = GameObject.Find("Continue").GetComponent<Button>();
        }

        async void Start()
        {
            HideThingsToBeginWith();

            await Signature();
            await Splash();

            await UntilClickOnContinue();

            await WhenAll(FadeOutAuthor(), FadeOutTitle());
        }

        Task FadeOutTitle()
        {
            return WhenAll
            (
                every.DOFade(0, .5f).SetEase(OutQuad).AsyncWaitForCompletion(),
                thing.DOFade(0, .5f).SetEase(OutQuad).AsyncWaitForCompletion(),
                @is.DOFade(0, .5f).SetEase(OutQuad).AsyncWaitForCompletion(),
                quiet.DOFade(0, .5f).SetEase(OutQuad).AsyncWaitForCompletion()
            );
        }

        async Task FadeOutAuthor()
        {
            await Sequence()
                .Append(gameBy.rectTransform.DOAnchorPosX(-1000, .5f).SetRelative(true))
                .Join(author.rectTransform.DOAnchorPosX(-1000, .5f).SetRelative(true))
                .AsyncWaitForCompletion();
        }

        async Task Splash()
        {
            await SpawnTitle();

            await TheMusicToStart();
            await SlideInAuthorFromLeft();

            await EndOfTheNextBar();
            await ScaleUpContinueButton();
        }

        async Task Signature()
        {
            await FadeInBackground();

            await FadeInSignature();
            await SyncWithTheme();
            await ScratchSignature();
            await FadeOutSignature();
        }

        async Task UntilClickOnContinue()
        {
            var fade = @continue.GetComponent<TMP_Text>().DOFade(1, .5f).SetAutoKill(false);
            await fade.AsyncWaitForCompletion();
            
            var tcs = new TaskCompletionSource<bool>();
            @continue.onClick.AddListener(() => tcs.SetResult(true));
            await tcs.Task;
            @continue.onClick.RemoveAllListeners();
            
            fade.PlayBackwards();
            await fade.AsyncWaitForCompletion();
        }

        async Task ScaleUpContinueButton()
        {
            @continue.transform.DOScaleY(1, 0f).Complete();
            await @continue.transform.DOScaleX(1, .25f).SetEase(OutBack).AsyncWaitForCompletion();

            await Sequence()
                .Append(Sequence()
                    .Append(@continue.transform.DOScale(1.05f, .2f).SetEase(Linear))
                    .Append(@continue.transform.DOScale(1, .2f).SetEase(Linear))
                    .AppendInterval(.25f)
                    .SetLoops(4, LoopType.Restart))
                .SetLoops(-1, LoopType.Restart)
                .AsyncWaitForElapsedLoops(1);
        }


        async Task ScratchSignature()
        {
            await Sequence()
                .Append(signature.transform.DORotate(Vector3.forward * -30, .1f).SetEase(OutQuad))
                .AppendInterval(.2f)
                .Append(signature.transform.DORotate(Vector3.forward * 15, .1f).SetEase(OutQuad))
                .Append(signature.transform.DORotate(Vector3.forward * 0, .1f).SetEase(OutQuad))
                .AsyncWaitForCompletion();
        }

        async Task SyncWithTheme()
        {
            await Delay(FromSeconds(1));
            audioSource.Play();
            await Delay(FromSeconds(.35));
        }

        async Task SpawnTitle()
        {
            await Delay(FromSeconds(.25f));
            Compose(every);
            await Delay(FromSeconds(.2f));
            await Compose(thing).AsyncWaitForCompletion();
            await Delay(FromSeconds(.1f));
            await Compose(@is).AsyncWaitForCompletion();
            await Delay(FromSeconds(.2f));
            await Compose(quiet).AsyncWaitForCompletion();

            Sequence Compose(TMP_Text text)
            {
                return Sequence()
                    .AppendCallback(() => text.alpha = 1)
                    .Append(text.DOFadeInCharEm(.1f).SetEase(OutExpo))
                    .AppendInterval(.2f);
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
            signature.DOFade(0, 0).Complete();
            return CompletedTask;
        }

        Task FadeInBackground()
        {
            return background.DOFade(1, 1f).From(0).SetEase(InOutSine).AsyncWaitForCompletion();
        }

        static Task TheMusicToStart() => Delay(FromSeconds(1.5));
        static Task EndOfTheNextBar() => Delay(FromSeconds(.5f));

        void HideThingsToBeginWith()
        {
            signature.DOFade(0, 0).Complete();

            every.DOFade(0, 0).Complete();
            thing.DOFade(0, 0).Complete();
            @is.DOFade(0, 0).Complete();
            quiet.DOFade(0, 0).Complete();

            gameBy.rectTransform.DOAnchorPosX(-1000, 0).Complete();
            author.rectTransform.DOAnchorPosX(-1000, 0).Complete();

            @continue.transform.DOScale(0, 0f).Complete();
        }
    }
}