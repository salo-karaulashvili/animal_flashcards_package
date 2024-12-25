using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globalController : MonoBehaviour
{
    [SerializeField] GameObject firstPart,SecondPart,confetti;
    [SerializeField] float nextStageLoadTime=3f;
    [SerializeField] Button exitButton;
    private bool doneOnce;
    private AnimalFlashcardsEntryPoint _entryPoint;
    void Start(){
        Camera.main.backgroundColor=new Color32(166,195,245,255);
        firstPart.SetActive(true);
        SecondPart.SetActive(false);
        exitButton.onClick.AddListener(finish);
    }

    private void finish()=> _entryPoint.InvokeGameFinished();

    public void SetEntryPoint(AnimalFlashcardsEntryPoint entryPoint)=>_entryPoint=entryPoint;

    void Update(){
        if(confetti.activeSelf&&!doneOnce) {
            Invoke("loadSecondPart",nextStageLoadTime);
            doneOnce=true;
        }
        if(doneOnce&&SecondPart.GetComponent<gameController2>().done){
            _entryPoint.InvokeGameFinished();
        }
    }
    void loadSecondPart(){
        Camera.main.backgroundColor=new Color32(165,236,254,255);
        SecondPart.SetActive(true);
        firstPart.SetActive(false);
    }
}
