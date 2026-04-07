using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CanvasCard : MonoBehaviour
{
    private Image imageUI;
    public Sprite backSprite;
    [HideInInspector]
    public Card card;
    private bool isSelected = false;
    private readonly float moveAmount = 25f;
    private readonly float duration = 0.2f;

    void Start()
    {
        imageUI = GetComponent<Image>();
    }

    public void Select()
    {
        MoveCard(true);
    }

    public void Unselect()
    {
        MoveCard(false);
    }
    
    public void Show()
    {
        imageUI.sprite = card.data.sprite;
    }

    public void Hide()
    {
        imageUI.sprite = backSprite;
    }
    
    public void Toggle()
    {
        if (!isSelected)
        {
            Select();
            CardManager.instance.AddSelectedCard(card);
        }
        else
        {
            Unselect();
            CardManager.instance.RemoveSelectedCard(card);
        }
        
        isSelected = !isSelected;
    }

    private void MoveCard(bool startingSelection)
    {
        transform.DOKill(true);

        float direction = startingSelection ? 1f : -1f;

        Vector3 movement = transform.up * (moveAmount * direction);

        transform.DOLocalMove(movement, duration).SetRelative(true).SetEase(Ease.OutCubic);
    }
}
