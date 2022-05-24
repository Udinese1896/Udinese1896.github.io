using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(count >=4)
        Destroy(this.gameObject);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Particle")
        {
            // Debug.Log("test");
            count++;
        }
    }

}