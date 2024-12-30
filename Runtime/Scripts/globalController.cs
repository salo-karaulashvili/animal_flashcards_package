using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class globalController : MonoBehaviour
{
    [SerializeField] GameObject firstPart,SecondPart,confetti;
    [SerializeField] float nextStageLoadTime=3f;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI fps;
    private bool doneOnce;
    private AnimalFlashcardsEntryPoint _entryPoint;
    private gameController2 gc2;
    void Start(){
        Camera.main.backgroundColor=new Color32(166,195,245,255);
        firstPart.SetActive(true);
        SecondPart.SetActive(false);
        exitButton.onClick.AddListener(finish);
        gc2=SecondPart.GetComponentInChildren<gameController2>();
    }

    private void finish(){_entryPoint.InvokeGameFinished(); Debug.Log("finished");}

    public void SetEntryPoint(AnimalFlashcardsEntryPoint entryPoint)=>_entryPoint=entryPoint;

    void Update(){
        fps.text=(int)(1 / Time.unscaledDeltaTime)+"";
        if(confetti.activeSelf&&!doneOnce) {
            Invoke("loadSecondPart",nextStageLoadTime);
            doneOnce=true;
        }
        if(doneOnce&&gc2.done){
            _entryPoint.InvokeGameFinished();
            Debug.Log("here");
        }
    }
    void loadSecondPart(){
        Camera.main.backgroundColor=new Color32(165,236,254,255);
        SecondPart.SetActive(true);
        firstPart.SetActive(false);
    }
}
