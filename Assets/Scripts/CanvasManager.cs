using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    static public CanvasManager instance;
    public List<CanvasCard> canvasHand = new();
    public List<GameObject> canvasBullets = new();
    public TextMeshProUGUI TextPlayerName;
    public TextMeshProUGUI TargetedCardText;
    public TextMeshProUGUI NumberOfCardsPlayedText;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayHand(List<Card> cards)
    {
        int index = 0;

        foreach (Card card in cards)  
        {
            CanvasCard canvasCard = canvasHand[index];

            canvasCard.card = card;
            canvasCard.isInteractable = true;
            canvasCard.Show();
            canvasCard.gameObject.SetActive(true);
            
            index++;
        }
    }

    public void HideHand()
    {
        foreach (CanvasCard canvasCard in canvasHand)  
        {
            canvasCard.gameObject.SetActive(false);
        }
    }

    public void UnselectHand()
    {
        foreach (CanvasCard canvasCard in canvasHand)
        {
            canvasCard.Unselect();
        }
    }

    public void ShakeHand()
    {
        foreach (CanvasCard canvasCard in canvasHand)
        {
            canvasCard.Shake();
        }
    }

    public void CallBluff()
    {
        GameManager.instance.CallBluff();
    }

    public void UpdatePlayerName(string name)
    {
        TextPlayerName.text = name;
    }

    public void UpdatePlayerLife(Player player)
    {
        int remainingChambers = player.revolver.GetRemainingChambers();

        foreach (GameObject bullet in canvasBullets)
        {
            bullet.SetActive(false);
        }

        for (int i = 0; i < remainingChambers && i < canvasBullets.Count; i++)
        {
            canvasBullets[i].SetActive(true);
        }
    }

    public void SetTargetedCardText(string text)
    {
        TargetedCardText.text = $"{text.ToUpper()}'S TABLE";
    }

    public void SetNumberOfCardsPlayedPreviousTurn(Player previousPlayerName, int numberOfCardsPlayed, string targetedCardName)
    {
        NumberOfCardsPlayedText.text = $"{previousPlayerName.name.ToUpper()}\n CLAIMS\n {numberOfCardsPlayed} x {targetedCardName.ToUpper()}";
    }
}
