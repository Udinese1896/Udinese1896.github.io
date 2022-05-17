using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class PlayerMovement2 : MonoBehaviour
{
    public NPCConversation TableConversation;
    public NPCConversation FirstConversation;
    public NPCConversation DoorConversation;
    private Rigidbody body;
    bool wDown;
    Animator anim;
    public GameObject FButton;
    public GameObject FE;//소화기
    public GameObject sPos;//발사 위치

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    private int interTablefirst = 0;
    private int DoorNum = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        ConversationManager.Instance.StartConversation(FirstConversation);
    }

    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive == false)
        {
            float _moveDirX = Input.GetAxisRaw("Horizontal");
            float _moveDirZ = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(_moveDirX, 0, _moveDirZ);

            body.MovePosition(transform.position + direction * Time.deltaTime * moveSpeed);
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(_moveDirX, _moveDirZ) * Mathf.Rad2Deg;

                // 부드러운 회전 
                body.rotation = Quaternion.Slerp(body.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.fixedDeltaTime);
            }

            if (_moveDirX != 0 || _moveDirZ != 0)
                anim.SetBool("IsRun", true);
            else
                anim.SetBool("IsRun", false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(FE, sPos.transform.position, sPos.transform.rotation);
            }


        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "NPC")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Talk with NPC");
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        FButton.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "DoorCube")
        {
            FButton.SetActive(false);
            ConversationManager.Instance.StartConversation(DoorConversation);
        }
    }

    void OnTriggerStay(Collider other)
    {
        FButton.SetActive(true);
        if (other.transform.tag == "Door")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("Building 2");
            }
        }

        if (other.transform.tag == "ComputerTable")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                ConversationManager.Instance.StartConversation(TableConversation);
            }
        }
    }
        void OnTriggerExit(Collider other)
        {
            FButton.SetActive(false);
        }
}