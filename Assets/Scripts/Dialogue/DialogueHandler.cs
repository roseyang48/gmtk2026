using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler instance;
    [SerializeField] private GameObject dialogueBox;
	[SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;
    [SerializeField] private Image leftSpeakerImage;
    [SerializeField] private Image rightSpeakerImage;
    [SerializeField] private GameObject leftDialogueObject;
    [SerializeField] private GameObject rightDialogueObject;
    private DialogueLine.SpeakerSide side;
    private int currChar = 0;
    public bool isAuto;
    public float autoTime;
    private float autoTimer;
    public float textSpeed;
    [SerializeField] private float skipSpeed;

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Update()
    {
        if(InputSystem.actions.FindAction("ToggleAuto").WasPressedThisFrame())
        {
            isAuto = !isAuto; //leftshift toggles auto mode
            autoTimer = 0f;
        }
    }
    public void TriggerDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(MoveThroughDialogue(dialogueObject));
    }
    public IEnumerator MoveThroughDialogue(DialogueObject dialogueObject)
    {
        //Shows elements
        int currDialogue = 0;
        dialogueBox.SetActive(true);
        while(currDialogue < dialogueObject.dialogueLines.Length)
        {
            //Sets lines and sprites
            DialogueLine currLine = dialogueObject.dialogueLines[currDialogue];
            //hide speaker image if none set
            if (currLine.side == DialogueLine.SpeakerSide.Left && currLine.speakerSprite != null)
            {
                leftDialogueObject.SetActive(true);
                rightDialogueObject.SetActive(false);
                side = DialogueLine.SpeakerSide.Left;
                leftSpeakerImage.sprite = currLine.speakerSprite;
            }
            else if (currLine.side == DialogueLine.SpeakerSide.Right && currLine.speakerSprite != null)
            {
                leftDialogueObject.SetActive(false);
                rightDialogueObject.SetActive(true);
                rightSpeakerImage.sprite = currLine.speakerSprite;
                side = DialogueLine.SpeakerSide.Right;
            }
            else if(currLine.side == DialogueLine.SpeakerSide.Left)
            {
                leftDialogueObject.SetActive(true);
                rightDialogueObject.SetActive(false);
                side = DialogueLine.SpeakerSide.Left;
            }
            else
            {
                leftDialogueObject.SetActive(false);
                rightDialogueObject.SetActive(true);
                side = DialogueLine.SpeakerSide.Right;
            }

            yield return DisplayText(currLine.dialogueText, currLine.seen);
            while(true)
            {
                yield return null;
                //Automatically plays next line without left click
                if(isAuto)
                {
                    autoTimer += Time.deltaTime;
                    if(autoTimer >= autoTime)
                    {
                        autoTimer = 0f;
                        currDialogue++;
                        currLine.seen = true;
                        break;
                    }
                }
                if(currLine.seen && InputSystem.actions.FindAction("Skip").IsPressed())
                {
                    yield return new WaitForSeconds(skipSpeed);
                    currDialogue++;
                    break;
                }
                else if(InputSystem.actions.FindAction("Interact").WasPressedThisFrame() || InputSystem.actions.FindAction("Scroll").ReadValue<float>() > 0)
                {
                    currDialogue++;
                    currLine.seen = true;
                    break;
                }
                else if(InputSystem.actions.FindAction("Scroll").ReadValue<float>() < 0 && currDialogue > 0)
                {
                    currDialogue--;
                    break;
                }
            }
        }
        dialogueBox.SetActive(false);
    }


    //shows text character by character
    private IEnumerator DisplayText(string text, bool seen)
    {
        float timer;
        bool skipText = false;
        while(currChar < text.Length)
        {
            timer = 0f;
            currChar++;
            TMP_Text dialogueText;
            if(side == DialogueLine.SpeakerSide.Left)
            {
                dialogueText = leftText;
            }
            else
            {
                dialogueText = rightText;
            }
            dialogueText.text = text.Substring(0, currChar);
            while(timer < textSpeed)
            {
                timer += Time.deltaTime;
                yield return null;
                if(InputSystem.actions.FindAction("Interact").WasPressedThisFrame() || (seen && InputSystem.actions.FindAction("Skip").IsPressed()))
                {
                    dialogueText.text = text;
                    skipText = true;
                    break;
                }
            }
            if(skipText)
            {
                break;
            }
        }
        currChar = 0;
    }
}

