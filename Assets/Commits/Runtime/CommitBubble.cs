using CommitionalConvents;
using LanguageExt;
using TMPro;
using UnityEngine;

namespace Commits.Runtime
{
    public class CommitBubble : MonoBehaviour
    {
        Option<Commit> represented;

        public void Free(int number, Commit model)
        {
            Setup(number, model);
            StartWandering(model);
        }

        void StartWandering(Commit model)
        {
            GetComponent<Wander>().Endlessly(mass: model.TotalSize);
        }

        void Setup(int number, Commit model)
        {
            represented = model;

            transform.localScale = Vector3.one * model.TotalSize;
            GetComponentInChildren<TMP_Text>().text = model.CommitType.id;

            GetComponentInChildren<SpriteRenderer>().color = model.Dye();
            
            GetComponentInChildren<SpriteRenderer>().sortingOrder = number * 10;
            GetComponentInChildren<TextMeshPro>().sortingOrder = number * 10 + 1;
        }
    }
}