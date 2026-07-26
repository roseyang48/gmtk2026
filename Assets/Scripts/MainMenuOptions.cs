using UnityEngine;

public class MainMenuOptions : MonoBehaviour
{
    [SerializeField] Animator optionsAnimator;
    public void OpenOptions()
    {
        optionsAnimator.SetTrigger("OpenOptions");
    }

    public void CloseOptions()
    {
        optionsAnimator.SetTrigger("CloseOptions");
    }
}
