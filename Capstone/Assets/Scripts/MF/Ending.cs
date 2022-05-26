using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    public NPCConversation EndingDialouge;
    public VideoPlayer video;
    public GameObject videoimage;
    private bool isPlaying = false;
    private bool bStop = false;
    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(EndingDialouge);
    }

    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive == false && isPlaying == false)
        {
            videoimage.SetActive(true);
            video.Play();
            isPlaying = true;
        }
    }
    public void OnPauseVideo()
    {
        if (bStop == false)
        {
            video.playbackSpeed = 0f;
            bStop = true;
        }
        else if(bStop==true)
        {
            video.playbackSpeed = 1f;
            bStop = false;
        }
    }
    public void restartVideo()
    {

            video.time = 0f;
            
    }
}
