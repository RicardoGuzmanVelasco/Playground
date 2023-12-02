using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Commits.Runtime
{
    public class CommitBar : MonoBehaviour
    {
        SharedModel Model => FindObjectOfType<SharedModel>();

        void Update()
        {
            UpdateButton();
            UpdateSlider();
            UpdateProportion();
        }

        void UpdateButton()
            => GetComponentInChildren<Button>().interactable = Model.CanCommit;

        void UpdateSlider()
            => GetComponentInChildren<Slider>().value
                = Model.IsStaging
                    ? Model.Staging.ProportionDone
                    : Model.ProportionToCommit;

        void UpdateProportion()
            => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Proportion").text
                = Model.IsStaging
                ? Proportion(Model.Staging.Eta, Model.Staging.TotalTimeToComplete)
                : Proportion(Model.Wip.TotalTimeSpent, Model.MinTimeToCommit);

        static string Proportion(float num, float den) => $"{num:0.00} / {den:0.00}";
    }
}