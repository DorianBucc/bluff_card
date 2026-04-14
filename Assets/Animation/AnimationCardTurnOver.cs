using UnityEngine;

public class AnimationCardTurnOver : MonoBehaviour
{
    public void HideCard()
    {
        CanvasManager.instance.cacherHand();
    }
    public void EndTurnAnimation()
    {
        GetComponent<Animator>().enabled = false;
        GameManager.instance.NextTurn();
    }
}
