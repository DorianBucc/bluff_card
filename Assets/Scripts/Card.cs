using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardTypeData typeCardData;
    public Image imageUI;
    public Sprite backSprite;
    public bool selected = false;

    public void Start()
    {
        Show();
    }

    public void Show()
    {
        imageUI.sprite = typeCardData.sprite;
    }

    public void Hide()
    {
        imageUI.sprite = backSprite;
    }
    public void selectCardToggle()
    {
        if (!selected)
        {
            //CardManager
        }
    }
}
