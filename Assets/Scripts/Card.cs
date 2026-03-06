using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardTypeData typeCardData;
    public Image imageUI;
    public Sprite backSprite;

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
}
