using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class gameController2 : MonoBehaviour
{
    [SerializeField] GameObject firtRound,secondRound,toFind,conffeti;
    [SerializeField] GameObject[] changables;
    [SerializeField] Transform dest,confetimask;
    [SerializeField] TextMeshPro animalName;
    private List<string> firstRoundAnimals=new List<string>(){"hippo","cow","giraffe","parrot","bee","deer","mouse"};
    private List<string> secondRoundAnimals=new List<string>(){"frog","bear","lion","pig","goat","dog","cat"};
    string lookingFor;
    private SpriteResolver sr;
    private AnimalFlashcardsEntryPoint _entryPoint;
    void Start(){
        sr=toFind.GetComponentInChildren<SpriteResolver>();
        firstRoundThings();
        generateNew();
    }

    public void SetEntryPoint(AnimalFlashcardsEntryPoint entryPoint)=>_entryPoint=entryPoint;
    void Update(){
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began){
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                if(hit){
                    string temp=hit.collider.tag;
                    if(temp==lookingFor){
                        Transform initalParent=hit.collider.transform.parent;
                        Vector2 animaPos=hit.collider.transform.position;
                        Transform curobj=hit.collider.transform;
                        curobj.parent=confetimask;
                        //ferad furadi efeqtebi
                        curobj.DOMove(dest.position,1f).SetEase(Ease.Linear).OnComplete(()=>{
                            curobj.gameObject.SetActive(false);
                            animalName.text=lookingFor.ToUpper();
                            toFind.transform.DOMove(Vector2.zero,2f).SetEase(Ease.Linear);
                            toFind.transform.DOScale(Vector2.one,2f).OnComplete(()=>{
                                //grublis animacia
                                toFind.transform.GetChild(2).transform.DOScale(Vector2.one,1f).OnComplete(()=>{
                                    GameObject tmp=Instantiate(conffeti,confetimask);
                                    tmp.transform.position=Vector3.zero;
                                    toFind.transform.GetChild(2).transform.DOScale(Vector2.zero,1f).SetDelay(2f);
                                    toFind.transform.DOScale(Vector2.one/2,2f).SetDelay(2f);
                                    toFind.transform.DOLocalMove(new Vector3(0,4,0),2f).SetEase(Ease.Linear).SetDelay(2f).OnComplete(()=>{
                                        curobj.DOMove(animaPos,0f).SetEase(Ease.Linear).OnComplete(()=>{
                                            curobj.transform.parent=initalParent;
                                            curobj.gameObject.SetActive(true);
                                            if(firstRoundAnimals.Count==0) secondRoundThings();
                                            if (secondRoundAnimals.Count==0) ending();
                                            generateNew();
                                            Destroy(tmp);
                                        });
                                    });
                                });
                            });
                        });
                    }
                    
                }
            }
        }
    }

    private void ending(){
        firtRound.SetActive(false);
        secondRound.SetActive(false);
        toFind.SetActive(false);
        _entryPoint.InvokeGameFinished();
    }

    void generateNew(){
        if(firstRoundAnimals.Count>0){
            int indx=UnityEngine.Random.Range(0,firstRoundAnimals.Count);
            lookingFor=firstRoundAnimals[indx];
            sr.SetCategoryAndLabel("first round",lookingFor);
            firstRoundAnimals.Remove(lookingFor);
        }
        else{
            int indx=UnityEngine.Random.Range(0,secondRoundAnimals.Count);
            lookingFor=secondRoundAnimals[indx];
            sr.SetCategoryAndLabel("second round",lookingFor);
            secondRoundAnimals.Remove(lookingFor);
        }
    }
    void firstRoundThings(){
        firtRound.SetActive(true);
        secondRound.SetActive(false);
        changables[0].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","3");
        changables[1].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","2");
        changables[2].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","1");
        changables[3].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","2");
        changables[4].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","1");
    }

    void secondRoundThings(){
        secondRound.SetActive(true);
        firtRound.SetActive(false);
        changables[0].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","1");
        changables[1].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","3");
        changables[2].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","2");
        changables[3].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","1");
        changables[4].GetComponent<SpriteResolver>().SetCategoryAndLabel("textures","2");
    }
}
