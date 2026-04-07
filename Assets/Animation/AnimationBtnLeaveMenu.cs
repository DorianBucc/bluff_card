using UnityEngine;

public class AnimationBtnLeaveMenu : MonoBehaviour
{
    public GameObject textGameObject;
    private Animator animatorBtn;

    void Start()
    {
        animatorBtn = GetComponent<Animator>();
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
