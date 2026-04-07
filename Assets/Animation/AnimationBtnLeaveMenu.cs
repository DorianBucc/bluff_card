using UnityEngine;

public class AnimationBtnLeaveMenu : MonoBehaviour
{
    public GameObject textGameObject;
    private Animator animatorBtn;
    public AnimationBtnSettingsMenu otherButtonSetting;

    void Start()
    {
        animatorBtn = GetComponent<Animator>();
    }

    public void ActivateOtherBtnSettings()
    {
        otherButtonSetting.ActivedAnimatedSet();
    }
    public void ActivedAnimated()
    {
        animatorBtn.enabled = true;
        textGameObject.SetActive(false);
    }
    public void DesactivedAnimated()
    {
        animatorBtn.enabled = false;
        textGameObject.SetActive(true);
    }

}
