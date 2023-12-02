using System;
using CommitionalConvents;
using TMPro;
using UnityEngine;

namespace Commits.Runtime
{
    public class CommitSpawn : MonoBehaviour
    {
        [SerializeField] CommitBubble commitPrefab;
        
        void Awake()
        {
            FindObjectOfType<SharedModel>().StagingCompleted += FreeSpawningCommit;
        }

        void FreeSpawningCommit(Commit commit)
        {
            Instantiate(commitPrefab, transform).Free(commit);
        }
    }
}