using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string scneneLoadedName;
    public void loadDesiredScene()
    {
        SceneManager.LoadScene(scneneLoadedName);
    }
}
