using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class PlayerMovement_Mountain : MonoBehaviour
{
    public NPCConversation FirstDialouge;
    public NPCConversation FirstSmokeDialouge;
    public NPCConversation SecondSmokeDialouge;
    public NPCConversation FirstNPCDialouge;
    public NPCConversation NPCDialouge;
    public NPCConversation FireDialouge;
    public NPCConversation FinalDialouge;
    public GameObject Smoke;
    public GameObject NPCS;
    public GameObject obstacle;
    private Rigidbody body;
    bool wDown;
    Animator anim;
    public GameObject FButton;
    private GameObject CamObject;


    //  public GameObject FE;//소화기
    //  public GameObject sPos;//발사 위치

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    private int interTablefirst = 0;
    private bool bFirstSmoke = false;
    private bool bSecondSmoke = false;
    private bool bFirstNPC = false;
    private bool bSecondNPC = false;
    private bool bFire = false;
    private bool bFinal = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        ConversationManager.Instance.StartConversation(FirstDialouge);
        CamObject = GameObject.Find("Main Camera");
        CamObject.GetComponent<BGMManger>().PlayBGM("BGM");
    }

    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive == false)
        {
            float _moveDirX = Input.GetAxisRaw("Horizontal");
            float _moveDirZ = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(_moveDirZ, 0, -_moveDirX);

            body.MovePosition(transform.position + direction * Time.deltaTime * moveSpeed);
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(_moveDirZ, -_moveDirX) * Mathf.Rad2Deg;

                // 부드러운 회전 
                body.rotation = Quaternion.Slerp(body.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.fixedDeltaTime);
            }

            if (_moveDirX != 0 || _moveDirZ != 0)
                anim.SetBool("IsRun", true);
            else
                anim.SetBool("IsRun", false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
              //  Instantiate(FE, sPos.transform.position, sPos.transform.rotation);
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
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Door")
        {
            if (bFirstSmoke == false)
            {
                ConversationManager.Instance.StartConversation(FirstSmokeDialouge);
                bFirstSmoke = true;
            }
        }

        if (other.transform.tag == "Smoke")
        {

            if (bFirstSmoke == true && bSecondSmoke == false)
            {
                ConversationManager.Instance.StartConversation(SecondSmokeDialouge);
                bSecondSmoke = true;
            }
            if (ConversationManager.Instance.IsConversationActive==false&&bFinal==false)
            {
                Smoke.SetActive(false);
            }
            else if(bFirstSmoke == true && bSecondSmoke == true && bFirstNPC == true && bSecondNPC == true && bFire == true&& bFinal==false)
            {
                ConversationManager.Instance.StartConversation(FinalDialouge);
                bFinal = false;
            }
            if (ConversationManager.Instance.IsConversationActive == false && bFinal == true)
            {
                //게임 종료
                Debug.Log("Game over");
            }
        }
        if (other.transform.name == "FirstNPCCube")
        {
            if (bFirstSmoke == true && bSecondSmoke == true && bFirstNPC == false)
            {
                ConversationManager.Instance.StartConversation(FirstNPCDialouge);
                bFirstNPC = true;
            }

        }


        if (other.transform.tag == "NPCCube")
        {
            if (bFirstSmoke == true && bSecondSmoke == true && bFirstNPC == true &&bSecondNPC==false)
            {
                ConversationManager.Instance.StartConversation(NPCDialouge);
                bSecondNPC = true;
            }
            if (ConversationManager.Instance.IsConversationActive == false)
            {
                NPCS.SetActive(false);
                obstacle.SetActive(false);
            }
        }

        if (other.transform.name == "FireCube")
        {
            if (bFirstSmoke == true && bSecondSmoke == true && bFirstNPC == true && bSecondNPC == true &&bFire==false)
            {
                CamObject.GetComponent<BGMManger>().PlayBGM("FIRE");
                ConversationManager.Instance.StartConversation(FireDialouge);
                bFire = true;
            }

        }





    }
    void OnTriggerExit(Collider other)
    {
        FButton.SetActive(false);
    }
}


