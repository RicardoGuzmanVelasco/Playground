using CommitionalConvents;
using LanguageExt;
using UnityEngine;

namespace Commits.Runtime
{
    public class CodeReview : MonoBehaviour
    {
        void Awake() => FindObjectOfType<CommitSpawn>().Committed += ListenToLink;

        void ListenToLink(CommitBubble commit) => commit.LinkedToIssue += PullRequest;

        void PullRequest(CommitBubble bubble, IssueTicket ticket)
        {
            bubble.Represented.IfSome(Link);

            void Link(Commit commit)
            {
                ticket.Represented.IfSome(issue =>
                {
                    var model = new CommitionalConvents.Producer();
                    Merge(bubble, ticket, model.Review(issue, commit));
                });
            }
        }

        void Merge(CommitBubble bubble, IssueTicket ticket, (Option<Issue> issue, Option<Commit> commit) review)
        {
            UpdateCommit(bubble, review.commit);
            UpdateTicket(ticket, review.issue);
        }

        void UpdateCommit(CommitBubble bubble, Option<Commit> reviewCommit)
            => reviewCommit.Match
            (
                Some: c => DiminishCommit(c, bubble),
                None: () => DestroyCommit(bubble)
            );

        void DiminishCommit(Commit commit, CommitBubble bubble)
        {
            throw new System.NotImplementedException();
        }

        void DestroyCommit(CommitBubble bubble)
        {
            bubble.Pop();
            // FindObjectOfType<SharedModel>().Origin
        }

        void UpdateTicket(IssueTicket ticket, Option<Issue> reviewIssue)
        {
            throw new System.NotImplementedException();
        }
    }
}