using System;
using System.Threading.Tasks;

namespace NoThanks.Runtime.Application
{
    public class TotallyRandom : PlayStrategy
    {
        public override async Task<bool> ListenIfTakeCard()
        {
            var randomWait = UnityEngine.Random.Range(.8f, 2f);
            await Task.Delay(TimeSpan.FromSeconds(randomWait));
            
            return new Random().Next(0, 4) == 0;
        }
    }
}