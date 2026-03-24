using System;

[Serializable]
public class Card
{
    public int id;
    public CardData data;

    public Card(int id, CardData data) {
        this.id = id;
        this.data = data;
    }
}
