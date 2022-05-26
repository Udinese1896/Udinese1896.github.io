using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class PlayerMovement_House : MonoBehaviour
{
    public NPCConversation FirstTableConversation;
    public NPCConversation SecondTableConversation;
    public NPCConversation TVTableConversation;
    private Rigidbody body;
    bool wDown;
    Animator anim;
    public GameObject FButton;

    public GameObject backPack;
    public GameObject backPackOnTable;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    private int interTablefirst = 0;
    private bool bCangoOutside = false;
    private bool bTVTable = false;
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

                // 부드러운 회전 
                body.rotation = Quaternion.Slerp(body.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.fixedDeltaTime);
            }

            if (_moveDirX != 0 || _moveDirZ != 0)
                anim.SetBool("IsRun", true);
            else
                anim.SetBool("IsRun", false);
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

    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Door")
        {
            if (bCangoOutside == true)
            {
                FButton.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.LoadScene("MF_Mountain");
                    FButton.SetActive(false);
                    Debug.Log("Interaction with Door");
                }
            }
        }

        if (other.transform.tag == "Table")
        {
            if (interTablefirst==0)
            FButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                FButton.SetActive(false);
                if (interTablefirst == 0)
                {
                    Debug.Log("Interaction with Table");
                    ConversationManager.Instance.StartConversation(FirstTableConversation);
                    interTablefirst += 1;
                    bCangoOutside = true;
                }
                else
                {
                    ConversationManager.Instance.StartConversation(SecondTableConversation);
                }
            }
            if (bCangoOutside == true && ConversationManager.Instance.IsConversationActive == false)
            {
                backPack.SetActive(true);
                backPackOnTable.SetActive(false);
            }
        }

        if (other.transform.tag == "TV")
        {
            if(bTVTable==false)
            {
                ConversationManager.Instance.StartConversation(TVTableConversation);
                bTVTable = true;
            }
        }





    }
    void OnTriggerExit(Collider other)
    {
        FButton.SetActive(false);
    }
}


