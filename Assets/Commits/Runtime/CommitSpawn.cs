using System;
using System.Linq;
using CommitionalConvents;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Commits.Runtime
{
    public class CommitSpawn : MonoBehaviour
    {
        [SerializeField] CommitBubble commitPrefab;
        
        public int CommitCount => FindObjectsOfType<CommitBubble>().Length;
        
        void Awake()
        {
            FindObjectOfType<SharedModel>().StagingCompleted += FreeSpawningCommit;
        }

        void FreeSpawningCommit(Commit commit)
        {
            Instantiate
            (
                commitPrefab,
                transform.position,
                Quaternion.identity,
                Origin()
            ).Free(number: CommitCount, commit);
        }

        static Transform Origin()
            => FindObjectsOfType<Transform>().Single(x => x.name == "Origin");
    }
}