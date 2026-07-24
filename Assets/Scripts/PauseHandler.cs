using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    InputAction pauseAction;
    [SerializeField] private Animator pauseScreenAnimator;
    private bool isPaused = false;
    private bool isOptions = false;
    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        pauseAction.performed += context => {Pause();};
    }

    public void Pause()
    {
        if(isOptions)
        {
            pauseScreenAnimator.SetTrigger("CloseOptions");
            isOptions = false;
        }
        else if(isPaused)
        {
            pauseScreenAnimator.SetTrigger("UnpauseGame");
            isPaused = !isPaused;
        }
        else
        {
            pauseScreenAnimator.SetTrigger("PauseGame");
            isPaused = !isPaused;
        }
    }

    public void OpenOptions()
    {
        pauseScreenAnimator.SetTrigger("OpenOptions");
        isOptions = true;
    }

    public void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }
}
