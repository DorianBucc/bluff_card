using UnityEngine;

public class AnimationBtnSettingsMenu : MonoBehaviour
{
    public Animator animatorBtn;
    void Start()
    {
        animatorBtn = GetComponent<Animator>();
    }

    public void ActivedAnimatedSet()
    {
        animatorBtn.enabled = true;
    }
    public void DesactivedAnimatedSet()
    {
        animatorBtn.enabled = false;
    }
}
