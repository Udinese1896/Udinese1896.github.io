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
    public NPCConversation SecondNPCDialouge;

    public NPCConversation FireDialouge;
    public NPCConversation FireMiddleDialouge;

    public NPCConversation FirstFEDialouge;
    public NPCConversation SecondFEDialouge;

    public NPCConversation FinalDialouge;

    public GameObject FEProj; //��ȭ�� ����ü
    public GameObject FEeffect; //��ȭ�� ����Ʈ
    public GameObject sPos; //��ȭ�� ����ü �߻� ��ġ
    public GameObject FButton; //��ư
    public GameObject MyFE; //��ȭ�� ������Ʈ(�տ� �����Ǿ��ִ�)
    public GameObject Fire; //��ȭ��� �� ��
    public GameObject Obstacle;//�ұ��� ���� ��ֹ�
    public GameObject Smoke;//������
    private float moveSpeed = 6.0f;
    private float rotationSpeed = 5.0f;
    private Rigidbody body;
    private GameObject CamObject;
    private Animator anim;


    private bool bFirstSmoke = false;
    private bool bSecondSmoke = false;
    private bool bFirstNPC = false;
    private bool bSecondNPC = false;
    private bool bFire = false;
    private bool bFireScond = false;
    private bool bFE = false;
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

                // �ε巯�� ȸ�� 
                body.rotation = Quaternion.Slerp(body.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.fixedDeltaTime);
            }

            if (_moveDirX != 0 || _moveDirZ != 0)
                anim.SetBool("IsRun", true);
            else
                anim.SetBool("IsRun", false);

            if (MyFE.activeSelf==true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(FEProj, sPos.transform.position, sPos.transform.rotation);
                    Instantiate(FEeffect, sPos.transform.position, sPos.transform.rotation);
                }
            }


        }

        if(Fire.gameObject.activeSelf==false&&MyFE.activeSelf == true)
        {
            ConversationManager.Instance.StartConversation(SecondFEDialouge);
            Obstacle.SetActive(false);
            MyFE.SetActive(false);
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
            else if(bFire == true&& bFinal==false)
            {
                ConversationManager.Instance.StartConversation(FinalDialouge);
                bFinal = false;
            }
            if (ConversationManager.Instance.IsConversationActive == false && bFinal == true)
            {
                //���� ����
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
            if (bFirstNPC == true &&bSecondNPC==false)
            {
                FButton.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    FButton.SetActive(false);
                    ConversationManager.Instance.StartConversation(SecondNPCDialouge);
                    bSecondNPC = true;
                }
            }
            if (ConversationManager.Instance.IsConversationActive == false&&bSecondNPC==true)
            {
                other.gameObject.SetActive(false);
            }
        }

        if (other.transform.name == "FireCube")
        {
            if (bSecondNPC == true &&bFire==false)
            {
                CamObject.GetComponent<BGMManger>().PlayBGM("FIRE");
                ConversationManager.Instance.StartConversation(FireDialouge);
                bFire = true;
            }

        }
        if (other.transform.name == "FireSecondCube")
        {
            if (bFire == true&& bFireScond==false)
            {
                ConversationManager.Instance.StartConversation(FireMiddleDialouge);
                bFireScond = true;
            }
        }

        if (other.transform.name == "FECube")
        {
            if (bFireScond == true&&bFE==false)
            {
                ConversationManager.Instance.StartConversation(FirstFEDialouge);
                MyFE.SetActive(true);
                bFE =true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        FButton.SetActive(false);
    }
}


