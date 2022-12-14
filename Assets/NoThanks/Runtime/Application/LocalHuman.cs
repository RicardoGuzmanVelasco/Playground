using System.Threading.Tasks;
using NoThanks.Runtime.Infrastructure;
using UnityEngine;

namespace NoThanks.Runtime.Application
{
    public class LocalHuman : PlayStrategy
    {
        public override Task<bool> ListenIfTakeCard()
        {
            return Object.FindObjectOfType<ListenInput>().ListenIfTakeCard();
        }
    }
}