using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletePopup : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {


    }

    public void OnContinueButtonClick()
    {
        transform.SendMessageUpwards("PopupContinueClicked");
        Debug.Log("Continue button clicked!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
