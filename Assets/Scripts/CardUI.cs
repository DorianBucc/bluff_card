using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image imageUI;
    public Sprite backSprite;
    public Card card;
    public bool isSelected = false;

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
            Hide();
            CardManager.instance.AddSelectedCard(card);
        }
        else
        {
            Show();
            CardManager.instance.RemoveSelectedCard(card);
        }
        
        isSelected = !isSelected;
    }
}
