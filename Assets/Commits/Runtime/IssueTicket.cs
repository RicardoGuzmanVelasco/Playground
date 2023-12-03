using CommitionalConvents;
using LanguageExt;
using TMPro;
using UnityEngine;

namespace Commits.Runtime
{
    public class IssueTicket : MonoBehaviour
    {
        Option<Issue> represented;
        
        public void Emerge(int number, Issue issue)
        {
            Setup(number, issue);
            StartWandering(issue);
        }

        void StartWandering(Issue issue) => GetComponent<Wander>().Endlessly(mass: issue.Size);

        void Setup(int number, Issue model)
        {
            represented = model;

            transform.localScale = Vector3.one * model.Size;
            GetComponentInChildren<TMP_Text>().text = model.IssueType.id;
            
            GetComponentInChildren<SpriteRenderer>().color = model.IssueType.counter.id.Dye();
            
            GetComponentInChildren<SpriteRenderer>().sortingOrder = number * 5;
            GetComponentInChildren<TextMeshPro>().sortingOrder = number * 5 + 1;
            
            name = $"Issue #{number}: {model.IssueType.id} ({model.Size})";
        }
    }
}