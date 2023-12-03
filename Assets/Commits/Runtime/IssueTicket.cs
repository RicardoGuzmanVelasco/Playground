using CommitionalConvents;
using DG.Tweening;
using LanguageExt;
using TMPro;
using UnityEngine;

namespace Commits.Runtime
{
    public class IssueTicket : MonoBehaviour
    {
        public Option<Issue> Represented { get; private set; }

        public void Emerge(int number, Issue issue)
        {
            Setup(number, issue);
            StartWandering(issue);
        }

        void StartWandering(Issue issue) => GetComponent<Wander>().Endlessly(mass: issue.Size);

        void Setup(int number, Issue model)
        {
            Resize(model);

            GetComponentInChildren<SpriteRenderer>().color = model.IssueType.counter.id.Dye();
            
            GetComponentInChildren<SpriteRenderer>().sortingOrder = number * 5;
            GetComponentInChildren<TextMeshPro>().sortingOrder = number * 5 + 1;
            
            name = $"Issue #{number}: {model.IssueType.id} ({model.Size})";
        }

        public void Resize(Issue model)
        {
            Represented = model;

            transform.localScale = Vector3.one * model.Size;
            GetComponentInChildren<TMP_Text>().text = model.IssueType.id;
        }

        public void Break()
        {
            Represented = Option<Issue>.None;
            GetComponent<Wander>().Stop();
            
            transform.DOScale(0, .33f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
        }
    }
}