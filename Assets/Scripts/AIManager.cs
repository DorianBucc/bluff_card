using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayRandom(Player player)
    {
        List<Card> playerCards = player.cards;

        int randomIndex = Random.Range(0, playerCards.Count);

        Card randomCard = playerCards[randomIndex];

        print("IA card: " + randomCard.typeData.cardName);

        CardManager cardManager = CardManager.instance;

        cardManager.AddSelectedCard(randomCard);
        cardManager.ConfirmSelectedCard();

        PlayerManager.instance.NextPlayer();
    }
}
