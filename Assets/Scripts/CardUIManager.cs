using System.Collections.Generic;
using UnityEngine;

public class CardUIManager : MonoBehaviour
{
    static public CardUIManager instance;
    public List<CardUI> deck = new();
    public List<CardTypeData> CardTypeDatas;

    public void Start()
    {
        List<Card> tempList = new();
        for(int i = 0; i < 5; i++)
        {
            Card card = new();
            int index = Random.Range(0, CardTypeDatas.Count - 1);
            card.id = index;
            card.typeData = CardTypeDatas[index];

            tempList.Add(card);
        }

        LoadPlayerDeck(tempList);
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadPlayerDeck(List<Card> cards)
    {
        int index = 0;

        foreach (Card card in cards)  
        {
            CardUI cardUI = deck[index];

            cardUI.card = card;
            cardUI.Show();
            
            index++;
        }
    }
}
