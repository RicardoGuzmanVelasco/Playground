using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Commits.Runtime
{
    public class CommitButton : MonoBehaviour
    {
        void Awake() => GetComponentInChildren<Button>().onClick.AddListener(Commit);
        void OnDestroy() => GetComponentInChildren<Button>().onClick.RemoveListener(Commit);

        async void Commit()
        {
            DisableAllButtons();
            
            var cooldown = FindObjectOfType<SharedModel>().Wip.TotalTimeSpent;
            await Task.Delay(TimeSpan.FromSeconds(cooldown) * Time.timeScale); 
            EnableAllButtons();
        }

        void DisableAllButtons() => FindObjectsOfType<Button>().ToList().ForEach(x => x.interactable = false);
        void EnableAllButtons() => FindObjectsOfType<Button>().ToList().ForEach(x => x.interactable = true);
    }
}