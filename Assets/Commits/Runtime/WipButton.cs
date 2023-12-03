using System;
using System.Linq;
using CommitionalConvents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Commits.Runtime
{
    public class WipButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        Commit.Type Represented => gameObject.name.ToLower().ToCommitType();
        bool producingWip;
        
        public void OnPointerDown(PointerEventData eventData) => producingWip = true;
        public void OnPointerUp(PointerEventData eventData) => producingWip = false;
        public void OnPointerExit(PointerEventData eventData) => producingWip = false;

        void Awake() => UpdateButtonType();
        
        void Update()
        {
            if(producingWip)
                FindObjectOfType<SharedModel>().Inject(Time.deltaTime, Represented);

            UpateTimerTo(SecondsSpent());
        }

        TimeSpan SecondsSpent()
            => TimeSpan.FromSeconds(FindObjectOfType<SharedModel>().Wip.TimeSpentOn(Represented));

        void UpateTimerTo(TimeSpan secondsSpent)
            => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "TimeSpent").text
                = secondsSpent.ToString(@"ss\:ff");
        
        void UpdateButtonType()
        {
            GetComponentsInChildren<TMP_Text>().Single(x => x.name == "CommitType").text = Represented.id;
            GetComponentInChildren<Image>().color = Represented.id.Dye();
        }
    }
}