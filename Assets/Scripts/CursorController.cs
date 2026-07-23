using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    InputAction select;
    InputAction cancel;
    InputAction look;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        select = InputSystem.actions.FindAction("Select");
        cancel = InputSystem.actions.FindAction("Cancel");
        look = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(look.ReadValue<Vector2>());
        cursorPosition.z = -1;
        transform.position = cursorPosition;

        if (select.WasPressedThisFrame())
        {
            RaycastHit2D[] results = Physics2D.RaycastAll(transform.position, Vector2.zero);
            for (int i = 0; i < results.Length; i++)
            {
                RaycastHit2D hit = results[i];
                if (hit.collider.GetComponent<RegionController>() != null)
                {
                    GameManager.Instance.RegionSelected(hit.collider.GetComponent<RegionController>().GetRegionNumber());
                }
            }
        }

        if (cancel.WasPressedThisFrame())
        {
            GameManager.Instance.CancelAction();
        }
    }
}
