using System.Collections;
using UnityEngine;

public class CountCounterAnimation : MonoBehaviour
{
    [SerializeField] GameObject counterObj;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float changeAmt;
    [SerializeField] ParticleSystem particles;
    public static CountCounterAnimation Instance;
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
        // particles.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(counterObj.transform.position.x, counterObj.transform.position.y));
    }

    public void InitializeCounter(int startingNumber)
    {
        counterObj.GetComponent<RectTransform>().anchorMax = new Vector2(counterObj.GetComponent<RectTransform>().anchorMax.x,
            counterObj.GetComponent<RectTransform>().anchorMax.y - changeAmt * startingNumber);
        counterObj.GetComponent<RectTransform>().anchorMin = new Vector2(counterObj.GetComponent<RectTransform>().anchorMin.x,
            counterObj.GetComponent<RectTransform>().anchorMin.y - changeAmt * startingNumber);
    }
    public void TickDown()
    {
        StartCoroutine(nameof(StartAnimation));
    }
    private IEnumerator StartAnimation()
    {
        RectTransform counterObjTransform = counterObj.GetComponent<RectTransform>();
        float timer = 0f;
        float startAnchorMin = counterObjTransform.anchorMin.y;
        float startAnchorMax = counterObjTransform.anchorMax.y;
        while (timer <= animCurve.keys[animCurve.keys.Length - 1].time)
        {
            timer += Time.deltaTime;
            float value = animCurve.Evaluate(timer) * changeAmt;
            counterObjTransform.anchorMin = new Vector2(counterObjTransform.anchorMin.x, startAnchorMin + value);
            counterObjTransform.anchorMax = new Vector2(counterObjTransform.anchorMax.x, startAnchorMax + value);
            yield return null;
        }
        particles.Play();
    }
}
