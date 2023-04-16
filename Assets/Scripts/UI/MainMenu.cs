using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Scenes/Scene_Blockout/Blockout_Level");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit the Game");
    }
}
