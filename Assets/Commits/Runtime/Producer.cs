using System;
using LanguageExt.SomeHelp;
using UnityEngine;

namespace Commits.Runtime
{
    public class Producer : MonoBehaviour
    {
        void Awake() => FindObjectOfType<CommitSpawn>().Committed += ListenToLink;

        void ListenToLink(CommitBubble commit) => commit.LinkedToIssue += PullRequest;

        void PullRequest(CommitBubble bubble, IssueTicket ticket)
        {
            Debug.Log($"Pull request from {bubble.name} to solve {ticket.name}");
        }
    }
}