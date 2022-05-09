using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get { return dialogManagerInstance; } }
    public TextMeshProUGUI dialogText;
    public Animator animator;
    public CanvasGroup canvasGroup;
    public Image dialogImageCharacter;

    private Queue<Sentence> sentences;
    private static DialogManager dialogManagerInstance;


    // Start is called before the first frame update
    private void Awake()
    {
        if (dialogManagerInstance != null && dialogManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            dialogManagerInstance = this;
        }
        sentences = new Queue<Sentence>();
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        foreach(Sentence sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);

        }
        animator.SetBool("Active", true);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GameManager.Instance.StopPlayer();
        DisplaySentence();
    }

    public void DisplaySentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            GameManager.Instance.StartPlayer();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        dialogImageCharacter.sprite = sentence.character;
        StopAllCoroutines();
        StartCoroutine(SetTextSlowly(sentence.text.GetLocalizedStringAsync().Result));
    }

    void EndDialogue()
    {
        animator.SetBool("Active", false);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator SetTextSlowly(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    } 
}
