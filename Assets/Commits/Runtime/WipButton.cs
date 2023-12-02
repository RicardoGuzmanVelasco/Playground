using System;
using System.Linq;
using CommitionalConvents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commits.Runtime
{
    public class WipButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] string commitType;

        Wip wipOfThisType = Wip.Begin();
        bool producingWip;

        public void OnPointerDown(PointerEventData eventData) => producingWip = true;
        public void OnPointerUp(PointerEventData eventData) => producingWip = false;
        public void OnPointerExit(PointerEventData eventData) => producingWip = false;

        void Update()
        {
            if(producingWip)
                wipOfThisType = wipOfThisType.Spend(Time.deltaTime, commitType.ToCommitType());

            UpateTimerTo(TimeSpan.FromSeconds(wipOfThisType.TotalTimeSpent));
        }

        void UpateTimerTo(TimeSpan secondsSpent)
            => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "TimeSpent").text
                = secondsSpent.ToString(@"ss\:ff");
    }
}