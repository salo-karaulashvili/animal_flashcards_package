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
    [SerializeField] float touchThreshhold=0.2f;
    [SerializeField] GameObject confetti;
    private List<bool> presentsOpen;
    private static int CARD_SIZE;
    int curIndx=5;
    private bool beagn;
    private Vector2 initTouchPosition;
    void Start(){
        CARD_SIZE=cards.Length;
        presentsOpen=new List<bool>(CARD_SIZE);
        for(int i=0;i<CARD_SIZE;i++){presentsOpen.Add(false);}
    }
    void Update(){
        if(Input.touchCount>0){
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began){
                beagn=true;
                initTouchPosition=touch.position;
            }else if(touch.phase==TouchPhase.Ended&&beagn){
                if(math.abs(initTouchPosition.x-touch.position.x)/Screen.width>touchThreshhold&&curIndx<CARD_SIZE&&curIndx>=0){
                    if(initTouchPosition.x-touch.position.x>0&curIndx!=CARD_SIZE-1) swipeRightToLeft();
                    else if(initTouchPosition.x-touch.position.x<0&&curIndx!=0) swipeLeftToRight();
                }
                if(cards[curIndx].GetComponent<present>().presentOpen){
                    presentsOpen[curIndx]=true;
                 }
            }
        }
        bool done=true;
        for(int i=0;i<presentsOpen.Count&&done;i++){done=done&&presentsOpen[i];}
        if(done&&!confetti.activeSelf) confetti.SetActive(true);
    }

    private void swipeLeftToRight(){
        cards[curIndx].transform.DOMove(rightPosShow.position,1f);
        cards[curIndx].transform.DOScale(0.5f,1f).SetEase(Ease.Linear);
        if(curIndx-1>=0) {
            cards[curIndx-1].transform.DOMove(new Vector3(0,0,0),1f);
            cards[curIndx-1].transform.DOScale(1,1f).SetEase(Ease.Linear);
        }
        if(curIndx-2>=0)cards[curIndx-2].transform.DOMove(leftPosShow.position,1f);
        if(curIndx+1<CARD_SIZE)cards[curIndx+1].transform.DOMove(rightPosOut.position,1f);
        curIndx--;
    }

    private void swipeRightToLeft(){
        cards[curIndx].transform.DOMove(leftPosShow.position,1f);
        cards[curIndx].transform.DOScale(0.5f,1f).SetEase(Ease.Linear);
        if(curIndx+1<CARD_SIZE) {
            cards[curIndx+1].transform.DOMove(new Vector3(0,0,0),1f);
            cards[curIndx+1].transform.DOScale(1,1f).SetEase(Ease.Linear);
        }
        if(curIndx+2<CARD_SIZE) cards[curIndx+2].transform.DOMove(rightPosShow.position,1f);
        if(curIndx-1>=0) cards[curIndx-1].transform.DOMove(leftPosOut.position,1f);
        curIndx++;
    }
}