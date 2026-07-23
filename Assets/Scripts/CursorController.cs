using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster[] graphicRaycasters;

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

        bool uiCollision = false;

        if (select.WasPressedThisFrame())
        {
            for (int i = 0; i < graphicRaycasters.Length; i++)
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
                {
                    position = look.ReadValue<Vector2>()
                };
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                graphicRaycasters[i].Raycast(pointerEventData, raycastResults);

                if (raycastResults.Count > 0)
                {
                    uiCollision = true;
                }
            }

            if (!uiCollision)
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
        }

        if (cancel.WasPressedThisFrame())
        {
            GameManager.Instance.CancelAction();
        }
    }
}
