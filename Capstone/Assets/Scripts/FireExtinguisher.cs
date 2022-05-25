using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
  //  public GameObject Effect;
    private float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Translate(Vector3.forward * 0.075f);
        if (time >= 2.0f)
            Destroy(transform.gameObject);
    }


}
