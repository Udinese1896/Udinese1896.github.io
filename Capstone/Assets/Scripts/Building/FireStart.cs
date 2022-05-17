using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStart : MonoBehaviour
{
    public GameObject Fire;
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
        if (other.tag == "Player")
        {
            Fire.SetActive(true);
        }
    }
}
