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

    public void Toggle()
    {
        if (isSelected)
        {
            Unselect();
            CardManager.instance.RemoveSelectedCard(card);
            return;
        }

        if (!CardManager.instance.CanSelectMore())
        {   
            Shake();
            return;
        }

        Select();
        CardManager.instance.AddSelectedCard(card);
    }

    public void Select()
    {
        MoveCard(true);
    }

    public void Unselect()
    {
        MoveCard(false);
    }

    public void Shake()
    {
        transform.DOKill(true);
        transform.DOShakePosition(0.2f, 10f, 20);
    }
    
    public void Show()
    {
        imageUI.sprite = card.data.sprite;
    }

    public void Hide()
    {
        imageUI.sprite = backSprite;
    }

    private void MoveCard(bool shouldSelect)
    {
        if (isSelected == shouldSelect) return;

        isSelected = shouldSelect;
        
        transform.DOKill(true);

        float direction = shouldSelect ? 1f : -1f;
        Vector3 movement = transform.up * (moveAmount * direction);

        transform.DOLocalMove(movement, duration).SetRelative(true).SetEase(Ease.OutCubic);
    }
}
