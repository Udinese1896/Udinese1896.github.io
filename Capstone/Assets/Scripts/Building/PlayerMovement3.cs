using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class PlayerMovement3 : MonoBehaviour
{
    public NPCConversation FirstConversation;
    public NPCConversation Npc1Conversation;
    public NPCConversation Npc2Conversation;
    public NPCConversation Npc3Conversation;
    public NPCConversation Npc4Conversation;
    public NPCConversation FireExtinguisher;
    public NPCConversation DoorConversation;
    private Rigidbody body;
    bool wDown;
    Animator anim;
    public GameObject FireE; // 소화기
    public GameObject FButton; // f버튼
    public GameObject FE;//소화기 파티클
    public GameObject sPos;//발사 위치

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    private bool FEON = false;
    private int NpcNum = 0;
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

                    Instantiate(FE, sPos.transform.position, sPos.transform.rotation);
                    FE.SetActive(true);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        FButton.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {

    }


    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Trigger")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                    FButton.SetActive(false);
                    ConversationManager.Instance.StartConversation(FireExtinguisher);
                    FireE.SetActive(true);
            }
        }

        if (other.transform.name == "Npc1")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                ConversationManager.Instance.StartConversation(Npc1Conversation);
            }
        }

        if (other.transform.name == "Npc2")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                ConversationManager.Instance.StartConversation(Npc2Conversation);
            }
        }

        if (other.transform.name == "Npc3")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                ConversationManager.Instance.StartConversation(Npc3Conversation);
            }
        }

        if (other.transform.name == "Npc4")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                ConversationManager.Instance.StartConversation(Npc4Conversation);
            }
        }

        if (other.transform.name == "DoorCube")
        {
            FButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (DoorNum == 0)
                {
                FButton.SetActive(false);
                ConversationManager.Instance.StartConversation(DoorConversation);
                DoorNum += 1;
                }
            }
        }

    }


    void OnTriggerExit(Collider other)
    {
        FButton.SetActive(false);
    }
}

