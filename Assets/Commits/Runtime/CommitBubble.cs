using System;
using CommitionalConvents;
using LanguageExt;
using TMPro;
using UnityEngine;

namespace Commits.Runtime
{
    public class CommitBubble : MonoBehaviour
    {
        Option<Commit> represented;

        public void Free(Commit model)
        {
            Setup(model);
            StartWandering(model);
        }

        void StartWandering(Commit model)
        {
            GetComponent<Wander>().Endlessly(speed: 1 / model.TotalSize);
        }

        void Setup(Commit model)
        {
            represented = model;

            transform.localScale = Vector3.one * model.TotalSize;
            GetComponentInChildren<TMP_Text>().text = model.CommitType.id;

            GetComponentInChildren<SpriteRenderer>().color = model.Dye();
        }
    }
}