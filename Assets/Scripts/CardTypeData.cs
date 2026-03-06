using UnityEngine;

[CreateAssetMenu(fileName = "CardTypeData", menuName = "Scriptable Objects/CardTypeData")]
public class CardTypeData : ScriptableObject
{
    public ECardType type;
    public string cardName;
    public Sprite sprite;
}
