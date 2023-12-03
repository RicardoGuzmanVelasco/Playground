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


                    void Merge(CommitBubble bubble, IssueTicket ticket,
                        (Option<Issue> issue, Option<Commit> commit) review)
                    {
                        UpdateCommit(bubble, review.commit);
                        UpdateTicket(ticket, review.issue);
                    }

                    void UpdateCommit(CommitBubble bubble, Option<Commit> reviewCommit)
                        => reviewCommit.Match
                        (
                            Some: c => DiminishCommit(c, bubble),
                            None: DestroyCommit
                        );

                    void DiminishCommit(Commit c, CommitBubble b)
                    {
                        b.Resize(c);
                        FindObjectOfType<SharedModel>().Drop(commit);
                        FindObjectOfType<SharedModel>().Push(c);
                    }

                    void DestroyCommit()
                    {
                        FindObjectOfType<SharedModel>().Drop(commit);
                        bubble.Pop();
                    }

                    void UpdateTicket(IssueTicket ticket, Option<Issue> reviewIssue)
                        => reviewIssue.Match
                        (
                            Some: i => { DiminishTicket(i, ticket); },
                            None: DestroyTicket
                        );


                    void DiminishTicket(Issue issue, IssueTicket issueTicket)
                    {
                        issueTicket.Resize(issue);
                        FindObjectOfType<SharedModel>().Close(issue);
                        FindObjectOfType<SharedModel>().Create(issue);
                    }

                    void DestroyTicket()
                    {
                        FindObjectOfType<SharedModel>().Close(issue);
                        ticket.Break();
                    }
                });
            }
        }
    }
}