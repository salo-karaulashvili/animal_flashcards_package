using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
public class swiping : MonoBehaviour
{
    [SerializeField] GameObject[] cards;
    [SerializeField] Transform leftPosOut,rightPosOut,leftPosShow,rightPosShow;
    private float touchThreshhold=0.05f;
    [SerializeField] GameObject confetti;
    private List<bool> presentsOpen;
    private static int CARD_SIZE;
    int curIndx=5;
    private bool beagn;
    private Vector2 initTouchPosition;
    private List<present> presents;
    void Start(){
        CARD_SIZE=cards.Length;
        presentsOpen=new List<bool>(CARD_SIZE);
        for(int i=0;i<CARD_SIZE;i++){presentsOpen.Add(false);}
        presents=new List<present>();
        for(int i=0;i<cards.Length;i++) presents.Add(cards[i].GetComponent<present>());
    }
    void Update(){
        if(Input.touchCount>0){
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began){
                beagn=true;
                initTouchPosition=touch.position;
            }else if(touch.phase==TouchPhase.Moved&&beagn){
                if(math.abs(initTouchPosition.x-touch.position.x)/Screen.width>touchThreshhold&&curIndx<CARD_SIZE&&curIndx>=0){
                    if(initTouchPosition.x-touch.position.x>0&curIndx!=CARD_SIZE-1) swipeRightToLeft();
                    else if(initTouchPosition.x-touch.position.x<0&&curIndx!=0) swipeLeftToRight();
                }
                if(math.abs(initTouchPosition.x-touch.position.x)/Screen.width>touchThreshhold)beagn=false;

            }
        }
        else if(Input.GetMouseButtonDown(0)){
            // beagn=true;
            // initTouchPosition=Input.mousePosition;
        }else if(Input.GetMouseButtonUp(0)&&beagn){
            // Vector2 mousePos=Input.mousePosition;
            // if(math.abs(initTouchPosition.x-mousePos.x)/Screen.width>touchThreshhold&&curIndx<CARD_SIZE&&curIndx>=0){
            //     if(initTouchPosition.x-mousePos.x>0&curIndx!=CARD_SIZE-1) swipeRightToLeft();
            //     else if(initTouchPosition.x-mousePos.x<0&&curIndx!=0) swipeLeftToRight();
            // }
            // if(cards[curIndx].GetComponent<present>().presentOpen){
            //     presentsOpen[curIndx]=true;
            // }
            // beagn=false;
        }
        bool done=true;
        for(int i=0;i<presents.Count&&done;i++){done=done&&presents[i].presentOpen;}
        if(done&&!confetti.activeSelf) confetti.SetActive(true);
    }

    float transitionTime=0.35f;
    private void swipeLeftToRight(){
        cards[curIndx].transform.DOMove(rightPosShow.position,transitionTime);
        cards[curIndx].transform.DOScale(0.5f,transitionTime).SetEase(Ease.Linear);
        if(curIndx-1>=0) {
            cards[curIndx-1].transform.DOMove(new Vector3(0,0,0),transitionTime);
            cards[curIndx-1].transform.DOScale(1,transitionTime).SetEase(Ease.Linear);
        }
        if(curIndx-2>=0)cards[curIndx-2].transform.DOMove(leftPosShow.position,transitionTime);
        if(curIndx+1<CARD_SIZE)cards[curIndx+1].transform.DOMove(rightPosOut.position,transitionTime);
        curIndx--;
    }

    private void swipeRightToLeft(){
        cards[curIndx].transform.DOMove(leftPosShow.position,transitionTime);
        cards[curIndx].transform.DOScale(0.5f,transitionTime).SetEase(Ease.Linear);
        if(curIndx+1<CARD_SIZE) {
            cards[curIndx+1].transform.DOMove(new Vector3(0,0,0),transitionTime);
            cards[curIndx+1].transform.DOScale(1,transitionTime).SetEase(Ease.Linear);
        }
        if(curIndx+2<CARD_SIZE) cards[curIndx+2].transform.DOMove(rightPosShow.position,transitionTime);
        if(curIndx-1>=0) cards[curIndx-1].transform.DOMove(leftPosOut.position,transitionTime);
        curIndx++;
    }
}
