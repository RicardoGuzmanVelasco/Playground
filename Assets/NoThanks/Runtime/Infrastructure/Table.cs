using System;
using System.Threading.Tasks;
using NoThanks.Runtime.Domain;
using TMPro;

namespace NoThanks.Runtime.Infrastructure
{
    public class Table
    {
        public Task SupplyCountersToPlayer(int howMany, Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"{player} ahora tiene {howMany} fichas";
            return Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}