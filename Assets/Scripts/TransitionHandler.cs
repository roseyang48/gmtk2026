using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    public static TransitionHandler Instance;
    
    [SerializeField] Animator transAnimator;
    [SerializeField] GameObject raycastBlocker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        raycastBlocker.SetActive(false);
    }
    // Update is called once per frame
    public void Transition()
    {
        raycastBlocker.SetActive(true);
        transAnimator.SetTrigger("StartTransition");
    }

    public void DisableRaycastBlocker()
    {
        raycastBlocker.SetActive(false);
        GameManager.Instance.StartNewTurn();
    }
}
