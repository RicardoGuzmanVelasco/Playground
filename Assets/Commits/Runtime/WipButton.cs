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

        bool producingWip;

        public void OnPointerDown(PointerEventData eventData) => producingWip = true;
        public void OnPointerUp(PointerEventData eventData) => producingWip = false;
        public void OnPointerExit(PointerEventData eventData) => producingWip = false;

        void Awake() => RenameButtonType();
        void OnValidate() => RenameButtonType();

        void Update()
        {
            if(producingWip)
                FindObjectOfType<SharedModel>().Inject(Time.deltaTime, commitType.ToCommitType());

            UpateTimerTo(SecondsSpent());
        }

        TimeSpan SecondsSpent()
            => TimeSpan.FromSeconds(FindObjectOfType<SharedModel>().Wip.TimeSpentOn(commitType.ToCommitType()));

        void UpateTimerTo(TimeSpan secondsSpent)
            => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "TimeSpent").text
                = secondsSpent.ToString(@"ss\:ff");
        
        void RenameButtonType() => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "CommitType").text = commitType;
    }
}