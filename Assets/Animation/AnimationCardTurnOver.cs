using UnityEngine;

public class AnimationCardTurnOver : MonoBehaviour
{
    public void HideCard()
    {
        CanvasManager.instance.cacherHand();
    }
    public void EndAnimation()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void EndTurnAnimation()
    {
        GameManager.instance.NextTurn();
    }
}
