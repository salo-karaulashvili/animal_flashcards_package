using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class present : MonoBehaviour
{
    [SerializeField] string animalName;
    public bool presentOpen;
    private Animator animator;
    private GameObject frame;
    void Start(){
        presentOpen=false;
        animator=GetComponentInChildren<Animator>();
        frame=transform.GetChild(2).gameObject;
        frame.transform.GetComponentInChildren<TextMeshPro>().text=animalName;
    }

    void Update(){
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began){
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                if(hit){
                    GameObject temp=hit.collider.gameObject;
                    if (temp.transform == transform&&temp.transform.localScale.x==1){
                        animator.SetTrigger("talking");
                        if(!presentOpen){
                            transform.GetChild(1).gameObject.SetActive(false);
                            presentOpen=true;
                        }
                    }
                }
            }
        }
        if(transform.localScale.x==1&&presentOpen){
            frame.transform.DOScale(Vector3.one,0.5f);
        }else if(transform.localScale.x<1){
            frame.transform.DOScale(Vector3.zero,0.5f);
        }
    }

    void OnMouseDown(){
        if(transform.localScale.x==1){
            animator.SetTrigger("talking");
            if(!presentOpen){
                transform.GetChild(1).gameObject.SetActive(false);
                presentOpen=true;
            }
        }
    }
}
