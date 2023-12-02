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
            => GetComponentInChildren<Slider>().value = Model.ProportionToCommit;
        
        void UpdateProportion()
            => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Proportion").text
                = $"{Model.Wip.TotalTimeSpent:0.00} / {Model.MinTimeToCommit:0.00}";
    }
}