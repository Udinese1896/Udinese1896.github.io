using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement_Mountain : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            this.transform.parent.transform.LookAt(collision.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            this.transform.parent.transform.LookAt(other.transform);
        }
    }
}
