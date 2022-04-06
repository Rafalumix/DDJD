using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HardcoreMode : MonoBehaviour
{
    static public bool hardcoreModeActivated = false; 
    public Text text; 

    void Start(){
        if (text != null){
             if (hardcoreModeActivated == true){
            text.text = "HARDCORE MODE ACTIVATED"; 
        } else {
            text.text = ""; 
        }
        }
       
    }

    public void ChangeBool(){
        if (hardcoreModeActivated == false){
            Debug.Log("Hardcore Mode activated!"); 
            hardcoreModeActivated = true; 
        } else {
            Debug.Log("Hardcore Mode disactivated!");
            hardcoreModeActivated = false; 
        }
    }

    public bool IsHardcore(){
        return hardcoreModeActivated; 
    }

    public void ChangeText(){
        if (text != null){
             if (hardcoreModeActivated == true){
            text.text = "HARDCORE MODE ACTIVATED"; 
        } else {
            text.text = ""; 
        }
    }}
}
