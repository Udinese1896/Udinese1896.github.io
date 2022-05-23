using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject RealFire;//บา

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            RealFire.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            RealFire.SetActive(false);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire")
        {
            RealFire.SetActive(false);
        }
    }
}