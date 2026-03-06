using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardTypeData cardTypeData;
    public Image imageUI;
    public Sprite backSprite;
    public bool isSelected = false;

    public void Start()
    {
        Show();
    }

    public void Show()
    {
        imageUI.sprite = cardTypeData.sprite;
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
            CardManager.instance.AddSelectedCard(cardTypeData);
        }
        else
        {
            Show();
            CardManager.instance.RemoveSelectedCard(cardTypeData);
        }
        
        isSelected = !isSelected;
    }
}
