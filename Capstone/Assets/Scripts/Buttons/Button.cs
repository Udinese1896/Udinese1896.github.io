using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{


    void Start()
    {

    }
    void Update()
    {

    }
    public void quit()
    {
        Application.Quit();
    }
    public void Story()
    {
        SceneManager.LoadScene("SelectMenu");
    }
    public void MF()
    {
        SceneManager.LoadScene("MF_House");
    }
    public void Building()
    {
        SceneManager.LoadScene("Building 1");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ReStart()
    {
        SceneManager.LoadScene("Building ending");
    }
}




