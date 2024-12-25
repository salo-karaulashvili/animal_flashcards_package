using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalController : MonoBehaviour
{
    [SerializeField] GameObject firstPart,SecondPart,confetti;
    [SerializeField] float nextStageLoadTime=3f;
    private bool doneOnce;
    void Start(){
        Camera.main.backgroundColor=new Color32(166,195,245,255);
        firstPart.SetActive(true);
        SecondPart.SetActive(false);
    }

    void Update(){
        if(confetti.activeSelf&&!doneOnce) {
            Invoke("loadSecondPart",nextStageLoadTime);
            doneOnce=true;
        }
    }
    void loadSecondPart(){
        Camera.main.backgroundColor=new Color32(165,236,254,255);
        SecondPart.SetActive(true);
        firstPart.SetActive(false);
    }
}
