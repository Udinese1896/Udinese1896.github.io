using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem ps;
    private int cnt =5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cnt<=0)
        {
            Destroy(transform.gameObject);
        }
    }



     void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PE")
        {
            Debug.Log("Test");
            cnt--;
        }
    }
}
