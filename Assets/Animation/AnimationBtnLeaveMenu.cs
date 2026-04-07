using UnityEngine;

public class AnimationBtnLeaveMenu : MonoBehaviour
{
    private Animator animatorBtn;

    void Start()
    {
        animatorBtn = GetComponent<Animator>();
    }
    public void ActivedAnimated()
    {
        animatorBtn.enabled = true;
    }
    public void DesactivedAnimated()
    {
        animatorBtn.enabled = false;
    }

}
