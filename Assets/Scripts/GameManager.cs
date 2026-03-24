using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCount = 2;
    public List<CardStack> cardStacks;
    private CardData SymbolCardRound;
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
        // TEMPORARY
        List<Player> players = new()
        {
            new(false, "Player", new List<Card>()),
            new(true, "IA", new List<Card>())
        };

        List<Card> cards = InitializeCards();

        PlayerManager playerManager = PlayerManager.instance;

        playerManager.InitializePlayers(players);

        playerManager.DealCards(cards, 5);

        InitializeSymbolCardRound();

        CardUIManager.instance.DisplayDeck(playerManager.currentPlayer.cards);
    }

    private List<Card> InitializeCards()
    {
        List<Card> cards = new();

        int id = 1;

        foreach (CardStack cardStack in cardStacks)
        {
            for (int i = 0; i < cardStack.quantity; i++)
            {
                Card card = new(id, cardStack.data);

                cards.Add(card);

                id++;
            }
        }

        return ShuffleCards(cards);
    }

    public void InitializeSymbolCardRound()
    {
        SymbolCardRound = cardStacks[Random.Range(0, cardStacks.Count)].data;
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

    public void CallLiar()
    {
        PlayerManager playerManager = PlayerManager.instance;

        Player accuser = playerManager.currentPlayer;
        Player accused = playerManager.GetPreviousPlayer();

        bool isHonest = StackManager.instance.CheckValidStack(SymbolCardRound);

        if (isHonest)
        {
            TextDebug.text = $"{accuser.name} lost";
            // playerCalling.Eliminate()
        }
        else
        {
            TextDebug.text = $"{accused.name} lost";
            // playerTarget.Eliminate()
        }

        // Malus appeler pour le perdant à créer
    }
}
