using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Commits.Runtime
{
    public class TechDebtBar : MonoBehaviour
    {
        void Update()
        {
            Slider().value = TechDebt();
            Fill().color = Color.Lerp(Color.green, Color.red, Slider().value);
        }

        Image Fill()
            => Slider()
                .GetComponentsInChildren<Image>()
                .Single(x => x.name == "Fill");

        Slider Slider() => GetComponentInChildren<Slider>();

        static float TechDebt()
            => FindObjectOfType<SharedModel>().Origin.TechDebtProportion;
    }
}