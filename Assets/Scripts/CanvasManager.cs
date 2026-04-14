using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    static public CanvasManager instance;
    public List<CanvasCard> canvasHand = new();
    public List<GameObject> canvasBullets = new();
    public CanvasCard stackCanvasCard;
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

    public void ShowStack()
    {
        stackCanvasCard.gameObject.SetActive(true);
    }

    public void HideStack()
    {
        stackCanvasCard.gameObject.SetActive(false);
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

    public void cacherHand() // Regarde pas tkt
    {
        foreach (CanvasCard canvasCard in canvasHand)  
        {
            canvasCard.Hide();
        }
    }
    public void HideHand()
    {
        foreach (CanvasCard canvasCard in canvasHand)  
        {
            canvasCard.gameObject.SetActive(false);
        }
    }

    public void ShowHand()
    {
        foreach (CanvasCard canvasCard in canvasHand)  
        {
            canvasCard.Show();
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

    public void SetNumberOfCardsPlayedPreviousTurn(string targetedCardName)
    {
        Player previousPlayerName = PlayerManager.instance.GetPreviousPlayer();
        int numberOfCardsPlayed = StackManager.instance.GetNumberOfCardsInStack();
        if(numberOfCardsPlayed > 0)
        {
            NumberOfCardsPlayedText.text = $"{previousPlayerName.name.ToUpper()}\n CLAIMS\n {numberOfCardsPlayed} x {targetedCardName}";
        }
        else
        {
            NumberOfCardsPlayedText.text = "Waiting For First Claim";
        }

    }

    public void MoveCardsToStack(List<Card> cards, System.Action onComplete)
    {   
        Sequence sequence = DOTween.Sequence();

        Vector3 targetPosition = stackCanvasCard.transform.position;
        Quaternion targetRotation = stackCanvasCard.transform.rotation;

        List<CanvasCard> cardsToAnimate = canvasHand.Where(cUI => cUI.gameObject.activeSelf && cards.Contains(cUI.card)).ToList();

        Dictionary<CanvasCard, Vector3> originalPositions = new();
        Dictionary<CanvasCard, Quaternion> originalRotations = new();

        foreach (CanvasCard cardUI in cardsToAnimate)
        {
            originalPositions[cardUI] = cardUI.transform.localPosition;
            originalRotations[cardUI] = cardUI.transform.localRotation;

            sequence.Join(cardUI.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutCubic));
            sequence.Join(cardUI.transform.DORotateQuaternion(targetRotation, 0.5f).SetEase(Ease.OutCubic));
        }

        sequence.OnComplete(() =>
        {
            ShowStack();

            foreach (CanvasCard cardUI in cardsToAnimate)
            {
                cardUI.gameObject.SetActive(false);

                cardUI.transform.DOKill();
                cardUI.transform.localPosition = originalPositions[cardUI];
                cardUI.transform.localRotation = originalRotations[cardUI];
            }

            onComplete?.Invoke();
        });
    }
}
