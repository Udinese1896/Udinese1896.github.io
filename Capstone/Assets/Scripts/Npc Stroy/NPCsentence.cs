using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsentence : MonoBehaviour
{
    public string[] sentences;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (DialogueManager.instance.dialoguegroup.alpha == 0)
                    DialogueManager.instance.Ondialogue(sentences);

            }
        }
    }

}
