using DG.Tweening;
using TMPro;

namespace EverythingIsQuiet.Infrastructure
{
    internal static class Extensions
    {
        public static Tweener DOFadeInCharEm(this TMP_Text text, float duration)
        {
            return DOTween.To(() => text.characterSpacing, x => text.characterSpacing = x, 0, duration).From(500)
                .SetEase(Ease.OutQuad);
        }
    }
}