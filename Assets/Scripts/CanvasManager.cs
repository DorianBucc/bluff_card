using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    static public CanvasManager instance;
    public List<CanvasCard> canvasHand = new();
    public TextMeshProUGUI TextPlayerName;

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

            canvasCard.Show();
            canvasCard.gameObject.SetActive(true);
            
            index++;
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

    public void HideHand()
    {
        foreach (CanvasCard canvasCard in canvasHand)  
        {
            canvasCard.gameObject.SetActive(false);
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
}
