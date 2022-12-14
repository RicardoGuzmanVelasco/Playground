using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoThanks.Runtime.Application;
using NoThanks.Runtime.Domain;
using TMPro;

namespace NoThanks.Runtime.Infrastructure
{
    public class Table
    {
        readonly Dictionary<Player, PlayStrategy> playerStrategies = new();
        
        public void Add(Player player, PlayStrategy strategy)
        {
            playerStrategies.Add(player, strategy);
        }

        public Task SupplyCountersToPlayer(int howMany, Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"{player} ahora tiene {howMany} fichas";
            return Task.Delay(TimeSpan.FromSeconds(.5f));
        }

        public async Task SetupDeck(Deck deck)
        {
                UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Ya han barajao.";
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
        }

        public async Task Show(Card card)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Ha salío el {card}";
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public Task<bool> ListenIfTakeCard(Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Qué haces, {player}";
            return playerStrategies[player].ListenIfTakeCard();
        }

        public Task GiveCardToPlayer(PlayingCard card, Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"{player} se come un {card} y {card.Counters} fichas";
            return Task.Delay(TimeSpan.FromSeconds(1.5f));
        }

        public Task PutCounterOnCard(PlayingCard card, Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"{player} ha puesto una ficha y ahora el {card} tiene {card.Counters} fichas";
            return Task.Delay(TimeSpan.FromSeconds(1.5f));
        }

        public Task NotifyGameOver()
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = "Ya estaría";
            return Task.Delay(TimeSpan.FromSeconds(1f));
        }

        public Task ShowPointsOf(Player player)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"{player} tiene {player.Points} puntos";
            return Task.Delay(TimeSpan.FromSeconds(1f));
        }

        public Task NotifyWinner(Player winner)
        {
            UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"Ha ganao {winner}";
            return Task.Delay(TimeSpan.FromSeconds(1f));
        }
    }
}