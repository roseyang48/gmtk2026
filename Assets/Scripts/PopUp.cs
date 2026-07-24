using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;
    public void Initialize(string text)
    {
        textBox.text = text;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
