using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAreaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            GameObject panel = GameObject.FindWithTag("Canvas").transform.Find("Panel").gameObject;
            if (panel == null)
            {
                return;
            }

            panel.SetActive(true);
        };
    }
}
