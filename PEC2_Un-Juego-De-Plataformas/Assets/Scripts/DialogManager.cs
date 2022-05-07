using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get { return dialogManagerInstance; } }
    public TextMeshProUGUI dialogText;
    public Animator animator;

    private Queue<string> sentences;
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
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        animator.SetBool("Active", true);
        GameManager.Instance.StopPlayer();
        DisplaySentence();
    }

    public void DisplaySentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            GameManager.Instance.StopPlayer();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(SetTextSlowly(sentence));

    }

    void EndDialogue()
    {
        animator.SetBool("Active", false);
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
