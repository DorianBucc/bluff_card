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

    void Start()
    {
        imageUI = GetComponent<Image>();
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
            Show();
            CardManager.instance.AddSelectedCard(card);
        }
        else
        {
            Hide();
            CardManager.instance.RemoveSelectedCard(card);
        }
        
        isSelected = !isSelected;
    }
}
