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

        void Setup(int number, Issue issue)
        {
            represented = issue;

            transform.localScale = Vector3.one * issue.Size;
            GetComponentInChildren<TMP_Text>().text = issue.IssueType.id;
            
            GetComponentInChildren<SpriteRenderer>().color = issue.IssueType.counter.id.Dye();
            
            GetComponentInChildren<SpriteRenderer>().sortingOrder = number * 5;
            GetComponentInChildren<TextMeshPro>().sortingOrder = number * 5 + 1;
        }
    }
}