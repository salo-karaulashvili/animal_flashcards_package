using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cloudAnimation : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] Transform min,max;
    float time;
    private List<GameObject> cloudsIn;
    Vector3 borderup,borderdown;
    void Start(){
        borderup=Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        borderdown=Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        spawnInitialClouds();
        InvokeRepeating("spawnClouds",0f,20f);
    }

    private void spawnInitialClouds(){
        for(int i=0;i<5;i++){
            time=Random.Range(110,130);
            int indx=Random.Range(0,clouds.Length);
            GameObject temp=Instantiate(clouds[indx],this.transform);
            float x=Random.Range(min.position.x,-max.position.x);
            float y=Random.Range(min.position.y,max.position.y);
            temp.transform.position=new Vector2(x,y);
            temp.transform.DOMoveX(-min.position.x+2f,time).SetEase(Ease.Linear).OnComplete(()=>{
                Destroy(temp.gameObject);
            });
        }
    }

    void spawnClouds(){
        int indx=Random.Range(0,clouds.Length);
        GameObject temp=Instantiate(clouds[indx],this.transform);
        float x=min.position.x;
        float y=Random.Range(min.position.y,max.position.y);
        temp.transform.position=new Vector2(x,y);
        time=Random.Range(110,130);
        temp.transform.DOMoveX(-min.position.x+2f,time).SetEase(Ease.Linear).OnComplete(()=>{
            Destroy(temp.gameObject);
        });
    }
}
