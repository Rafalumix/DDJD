using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject exitButton;

    void Start() {
        #if (!UNITY_WEBGL)
            exitButton.SetActive(true);
        #endif
    }
    public void PlayGame(){
        SceneManager.LoadScene("mainScene"); 
    }

    public void QuitGame(){
        Application.Quit(); 
    }
}
