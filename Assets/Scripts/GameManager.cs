using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        PlayerManager.instance.NextGameTurn();
    }

    public void NextGameTurn()
    {
        CardManager.instance.ConfirmCardSelected();
    }

    public void EndRound(){}
    public void EndRoundLiar(){}
}
