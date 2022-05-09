using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialog dialog;

    public void TriggerDialoge()
    {
        DialogManager.Instance.StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerDialoge();
            Destroy(this);
        }
    }

}
