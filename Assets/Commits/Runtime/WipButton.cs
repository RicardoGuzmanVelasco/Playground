using System;
using CommitionalConvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                wipOfThisType = wipOfThisType.Spend(Time.deltaTime, Commit.Type.Feat);
        }
    }

    public static class CommitBridge
    {
        // public static Commit.Type 
    }
}