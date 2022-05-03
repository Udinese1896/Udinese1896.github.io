using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerClickHandler
{
    public Text DialogueText;
    public GameObject NextText;
    public CanvasGroup dialoguegroup;
    public Queue<string> sentences; // Queue ����

    private string currentSentence;

    public float typingSpeed = 0.1f;

    private bool istyping;

    public static DialogueManager instance;
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        sentences = new Queue<string>(); // sentences�� �ʱ�ȭ
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear(); // Ȥ�� ť�� �ִ� �����͸� ����
        foreach (string line in lines)
        {
            sentences.Enqueue(line); // ť�� �޼���� �ֱ�
        }
        dialoguegroup.alpha = 1; //��ȭâ�� Ű��
        dialoguegroup.blocksRaycasts = true; // bloacksRaycasts�� true�϶� ���콺 �̺�Ʈ�� �����մϴ�.

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            //�ڷ�ƾ.
            istyping = true;
            NextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }

        else
        {
            dialoguegroup.alpha = 0; // ��ȭâ�� ����
            dialoguegroup.blocksRaycasts = false; // bloacksRaycasts�� true�϶� ���콺 �̺�Ʈ�� �����մϴ�.
        }
    }

    IEnumerator Typing(string line)
    {
        DialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    void Update()
    {
        // dialoueText == currentSentence ��� ���� ��.

        if (DialogueText.text.Equals(currentSentence))
        {

            NextText.SetActive(true);
            istyping = false;
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!istyping)
                    NextSentence();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!istyping)
            NextSentence();
    }

    private void OnCollisionStay(Collision collision)
    {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!istyping)
                    NextSentence();
            }
        }

}
