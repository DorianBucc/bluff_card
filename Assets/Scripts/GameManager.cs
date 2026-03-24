using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCount = 2;
    public List<CardTypeSlot> cardTypeSlots;
    private CardTypeData SymbolCardRound;
    public TextMeshProUGUI TextSymbolCardRound;
    public TextMeshProUGUI TextDebug;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        // TEMP
        List<Player> players = new()
        {
            new(new List<Card>(), false),
            new(new List<Card>(), true)
        };

        // for (int i = 0; i < playerCount; i++)
        // {
        //     Player player = new();

        //     players.Add(player);
        // }

        List<Card> cards = InitializeCards();

        PlayerManager playerManager = PlayerManager.instance;

        playerManager.InitializePlayers(players);

        playerManager.DealCards(cards, 5);

        CardUIManager.instance.DisplayDeck(playerManager.currentPlayer.cards);
    }

    private List<Card> InitializeCards()
    {
        List<Card> cards = new();

        int id = 1;

        foreach (var cardTypeSlot in cardTypeSlots)
        {
            for (int i = 0; i < cardTypeSlot.quantity; i++)
            {
                Card card = new(id, cardTypeSlot.cardType);

                cards.Add(card);

                id++;
            }
        }

        InitializeSymbolCardRound();
        return ShuffleCards(cards);
    }

    public void InitializeSymbolCardRound()
    {
        SymbolCardRound = cardTypeSlots[Random.Range(0,cardTypeSlots.Count)].cardType;
        TextSymbolCardRound.text = SymbolCardRound.cardName;
    }

    public void NextTurn()
    {
        CardManager.instance.ConfirmSelectedCard();
        PlayerManager.instance.NextPlayer();
    }

    private List<Card> ShuffleCards(List<Card> cardsToShuffle)
    {
        List<Card> shuffledCards = new(cardsToShuffle);

        for (int i = shuffledCards.Count - 1; i >= 0 ; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            (shuffledCards[randomIndex], shuffledCards[i]) = (shuffledCards[i], shuffledCards[randomIndex]);
        }

        return shuffledCards;
    }

    public void callLiar()
    {
        Player playerCalling = PlayerManager.instance.currentPlayer;
        // Player playerTarget = ;


        if (StackManager.instance.checkValidStack(SymbolCardRound))
        {
            TextDebug.text = "Player actuel perdu";
            // playerCalling.Dead()
        }
        else
        {
            TextDebug.text = "Player précedent perdu";
            // playerTarget.Dead()
        }

        // Malus appeler pour le perdant à créer
    }
}
