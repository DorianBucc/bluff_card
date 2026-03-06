using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardTypeData typeCardData;
    public Image imageUI;

    public void Start()
    {
        imageUI.sprite = typeCardData.sprite;
    }
}
