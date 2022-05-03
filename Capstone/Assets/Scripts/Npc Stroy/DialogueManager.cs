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
    public Queue<string> sentences; // Queue 구현

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
        sentences = new Queue<string>(); // sentences를 초기화
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear(); // 혹시 큐에 있는 데이터를 비우기
        foreach (string line in lines)
        {
            sentences.Enqueue(line); // 큐의 메서드로 넣기
        }
        dialoguegroup.alpha = 1; //대화창을 키고
        dialoguegroup.blocksRaycasts = true; // bloacksRaycasts가 true일때 마우스 이벤트를 감지합니다.

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            //코루틴.
            istyping = true;
            NextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }

        else
        {
            dialoguegroup.alpha = 0; // 대화창을 끄고
            dialoguegroup.blocksRaycasts = false; // bloacksRaycasts가 true일때 마우스 이벤트를 감지합니다.
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
        // dialoueText == currentSentence 대사 한줄 끝.

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
