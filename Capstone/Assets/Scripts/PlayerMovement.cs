using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody body;
    bool wDown;
     Animator anim;

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;

    void Start()
    {
          anim=GetComponent<Animator>();
          body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(_moveDirX, 0, _moveDirZ);

        body.MovePosition(transform.position + direction * Time.deltaTime * moveSpeed);
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(_moveDirX, _moveDirZ) * Mathf.Rad2Deg;

            // 즉시 회전
            //body.rotation = Quaternion.Euler(0, angle, 0);

            // 부드러운 회전 
              body.rotation = Quaternion.Slerp(body.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.fixedDeltaTime);      
        }

        if (_moveDirX != 0|| _moveDirZ != 0)
            anim.SetBool("IsRun", true);
        else
            anim.SetBool("IsRun", false);
    }
}


