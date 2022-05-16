using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class PlayerMovement2 : MonoBehaviour
{
    public NPCConversation FirstTableConversation;
    public NPCConversation SecondTableConversation;
    public NPCConversation DoorConversation;
    private Rigidbody body;
    bool wDown;
    Animator anim;
    public GameObject FButton;
    public GameObject FE;//��ȭ��
    public GameObject sPos;//�߻� ��ġ

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    private int interTablefirst = 0;
    private int DoorNum = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
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

                // �ε巯�� ȸ�� 
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
        FButton.SetActive(true);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Door")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (DoorNum == 0)
                {
                    FButton.SetActive(false);
                    ConversationManager.Instance.StartConversation(DoorConversation);
                    Debug.Log("Interaction with Door");
                    DoorNum += 1;
                }
                else
                {
                    SceneManager.LoadScene("Building 2");
                }
            }
        }


        if (other.transform.tag == "ComputerTable")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                if (interTablefirst == 0)
                {

                    Debug.Log("Interaction with Cube");
                    ConversationManager.Instance.StartConversation(FirstTableConversation);
                    interTablefirst += 1;
                }
                else
                {
                    ConversationManager.Instance.StartConversation(SecondTableConversation);
                }
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        FButton.SetActive(false);
    }
}