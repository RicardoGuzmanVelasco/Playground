using System;
using CommitionalConvents;
using DG.Tweening;
using LanguageExt;
using TMPro;
using UnityEngine;

namespace Commits.Runtime
{
    public class CommitBubble : MonoBehaviour
    {
        public Option<Commit> Represented { get; private set; }

        public event Action<CommitBubble, IssueTicket> LinkedToIssue;

        public void Free(int number, Commit model)
        {
            Setup(number, model);
            StartWandering(model);
        }

        void StartWandering(Commit model) => GetComponent<Wander>().Endlessly(mass: model.TotalSize);

        void Setup(int number, Commit model)
        {
            Represented = model;

            transform.localScale = Vector3.one * model.TotalSize;
            GetComponentInChildren<TMP_Text>().text = model.CommitType.id;

            GetComponentInChildren<SpriteRenderer>().color = model.Dye();
            
            GetComponentInChildren<SpriteRenderer>().sortingOrder = number * 10;
            GetComponentInChildren<TextMeshPro>().sortingOrder = number * 10 + 1;
            
            name = $"Commit #{number}: {model.CommitType.id} ({model.TotalSize})";
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var ticket = other.GetComponent<IssueTicket>();
            if(ticket is null)
                return;
            
            LinkedToIssue?.Invoke(this, ticket);
        }

        public void Pop()
        {
            Represented = Option<Commit>.None;
            GetComponent<Wander>().Stop();
            
            GetComponentInChildren<SpriteRenderer>().DOFade(0, .25f);
            GetComponentInChildren<TMP_Text>().DOFade(0, .25f);
            transform.DOScale(2, .33f).SetEase(Ease.InBounce).SetRelative(true).OnComplete(() => Destroy(gameObject));
        }
    }
}