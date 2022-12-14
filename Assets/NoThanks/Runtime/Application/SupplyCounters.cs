using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoThanks.Runtime.Domain;
using NoThanks.Runtime.Infrastructure;

namespace NoThanks.Runtime.Application
{
    public class SupplyCounters
    {
        readonly Table view;
        
        public SupplyCounters(Table view)
        {
            this.view = view;
        }

        public async Task Execute(params Player[] players)
        {
            var counters = players.Length switch
            {
                >= 3 and <= 5 => 11,
                6 => 9,
                7 => 7,
                _ => throw new NotSupportedException()
            };

            foreach(var player in players)
            {
                player.SupplyCounters(counters);
                await view.SupplyCountersToPlayer(counters, player);
            }
        }
    }
}