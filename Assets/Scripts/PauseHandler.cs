using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    InputAction pauseAction;
    [SerializeField] private Animator pauseScreenAnimator;
    private bool isPaused = false;
    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        pauseAction.performed += context => {Pause();};
    }

    public void Pause()
    {
        if(isPaused)
        {
            pauseScreenAnimator.SetTrigger("UnpauseGame");
        }
        else
        {
            pauseScreenAnimator.SetTrigger("PauseGame");
        }
        isPaused = !isPaused;
    }

    public void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }
}
