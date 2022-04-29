using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player") 
        {
            if(Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("MF_Mountain");

        }
    }

    
}
