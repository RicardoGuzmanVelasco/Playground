using System;
using System.Threading.Tasks;
using UnityEngine;

namespace NoThanks.Runtime.Infrastructure
{
    public class ListenInput : MonoBehaviour
    {
        TaskCompletionSource<KeyCode> tcs;

        public async Task<bool> ListenIfTakeCard()
        {
            tcs = new TaskCompletionSource<KeyCode>();

            var result = await tcs.Task;

            return result == KeyCode.RightArrow;
        }
        
        void Update()
        {
            if(tcs is null)
                return;
            
            if(Input.GetKeyDown(KeyCode.LeftArrow))
                tcs.SetResult(KeyCode.LeftArrow);
            if(Input.GetKeyDown(KeyCode.RightArrow))
                tcs.SetResult(KeyCode.RightArrow);
        }
    }
}