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
            return Task.Delay(TimeSpan.FromSeconds(.5f));
        }

        public async Task FormDeck(Deck deck)
        {
                UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Ya han barajao.";
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
        }

        public async Task Show(Card card)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Ha salío el {card}";
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public Task<bool> ListenIfTakeCard(Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Qué haces, {player}";
            return UnityEngine.Object.FindObjectOfType<ListenInput>().ListenIfTakeCard();
        }
    }
}