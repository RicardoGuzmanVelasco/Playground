using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;

namespace Commits.Runtime
{
    public class CommitButton : MonoBehaviour
    {
        SharedModel Model => FindObjectOfType<SharedModel>();
        
        void Awake() => GetComponentInChildren<Button>().onClick.AddListener(Commit);
        void OnDestroy() => GetComponentInChildren<Button>().onClick.RemoveListener(Commit);

        async void Commit()
        {
            DisableAllButtons();
            
            var cooldown = Model.Wip.TotalTimeSpent;
            Model.Commit();
            
            await Delay(FromSeconds(cooldown * Time.timeScale));
            EnableAllButtons();
        }

        void DisableAllButtons() => FindObjectsOfType<Button>().ToList().ForEach(x => x.interactable = false);
        void EnableAllButtons() => FindObjectsOfType<Button>().ToList().ForEach(x => x.interactable = true);
    }
}