using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if _DEBUG_AVAILABLE__

using UnityEditor;

#endif

public class GameManager : MonoBehaviour
{
    public Transform[] dialogCommon;
    public Transform[] dialogCharacters;
    public Transform dialogText;

    [System.Serializable]
    public struct DialogData
    {
        public int character;
        public string text;
    };

    public DialogData[] dialogData;

    bool showingDialog = false;

    TextMeshPro dialogTextC;

    public int dialogIndex;


    KeyCode[] debugKey = { KeyCode.LeftControl, KeyCode.T };
    int debugKeyProgress = 0;

    void Start()
    {
        showingDialog = false;

        dialogTextC = dialogText.GetComponent<TextMeshPro>();
    }
#if __DEBUG__AVAILABLE__
    void OnDrawGizmos()
    {
        if (Switches.debugMode && Switches.debugDialogs)
        {
            if (showingDialog)
            {
                Handles.color = Color.white;
                Handles.Label(dialogText.position - Vector3.up * 1.0f, "Dialog Id: " + dialogIndex);
            }
            
        }
        
    }
#endif
    // Update is called once per frame
    void Update()
    {
#if __DEBUG_AVAILABLE__
        if(Switches.debugMode && Switches.debugDialogs)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                showingDialog = true;
                dialogIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                dialogIndex = (dialogIndex + 1) % dialogData.Length;
            }
        }
#endif

        if (showingDialog)
        {
            for (int i = 0; i < dialogCommon.Length; i++) dialogCommon[i].gameObject.SetActive(true);
            for (int i = 0; i < dialogCharacters.Length; i++) dialogCharacters[i].gameObject.SetActive(false);

            int character = dialogData[dialogIndex].character;
            string text = dialogData[dialogIndex].text;

            dialogCharacters[character].gameObject.SetActive(true);
            dialogTextC.text = text;


            if (Input.GetKeyDown(KeyCode.Return))
            {
                showingDialog = false;
            }

        }else
        {
            for (int i = 0; i < dialogCommon.Length; i++) dialogCommon[i].gameObject.SetActive(false);
            for (int i = 0; i < dialogCharacters.Length; i++) dialogCharacters[i].gameObject.SetActive(false);


        }
#if ___DEBUG__AVAILABLE__
        if (!Switches.debugMode)
        {
            //if (Input.GetKeyDown(debugKey[debugKeyProgress]))
            //{
            //    //debugKeyProgres++;
            //    if(debugKeyProgress == debugKey.Length)
            //    {
            //        Switches.debugMode = true;
            //        Debug.Log("Debug mode on");
            //    }

            //}
        }
#endif
    }

    public void OnTriggerDialog(int index)
    {
        showingDialog = true;
        dialogIndex = index;
    }

    public bool IsShowingDialog()
    {
        return showingDialog;
    }
}
