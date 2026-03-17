using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        PlayerManager.instance.NextTurn();
    }

    public void NextGameTurn()
    {
        CardManager.instance.ConfirmCardSelected();
    }

    public void EndRound() {}
    
    public void EndRoundLiar() {}
}
