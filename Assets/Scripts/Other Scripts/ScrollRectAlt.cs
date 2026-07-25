using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollRectAlt : ScrollRect, IPointerEnterHandler, IPointerExitHandler
{
    private bool swallowMouseWheelScrolls = true;
    private bool isMouseOver = false;

    float scrollDeadZone = 0.1f;

    InputAction scroll;

    private float scrollModifier = 2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    protected override void Start()
    {
        scroll = InputSystem.actions.FindAction("Scroll");
    }

    private void Update()
    {
        // Detect the mouse wheel and generate a scroll. This fixes the issue where Unity will prevent our ScrollRect
        // from receiving any mouse wheel messages if the mouse is over a raycast target (such as a button).
        if (isMouseOver && scroll.WasPerformedThisFrame())
        {
            var delta = scroll.ReadValue<float>();

            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.scrollDelta = new Vector2(0f, delta);

            swallowMouseWheelScrolls = false;
            OnScroll(pointerData);
            swallowMouseWheelScrolls = true;
        }
    }

    public override void OnScroll(PointerEventData data)
    {
        if (scroll.WasPerformedThisFrame() && swallowMouseWheelScrolls)
        {
            // Eat the scroll so that we don't get a double scroll when the mouse is over an image
        }
        else
        {
            // Amplify the mousewheel so that it matches the scroll sensitivity.
            if (data.scrollDelta.y < -Mathf.Epsilon)
                data.scrollDelta = new Vector2(0f, -scrollSensitivity) * scrollModifier;
            else if (data.scrollDelta.y > Mathf.Epsilon)
                data.scrollDelta = new Vector2(0f, scrollSensitivity) * scrollModifier;

            base.OnScroll(data);
        }
    }
}
