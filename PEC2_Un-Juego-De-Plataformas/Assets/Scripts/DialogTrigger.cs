using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    
    public void TriggerDialoge()
    {
        DialogManager.Instance.StartDialog(dialog);
    }

}
