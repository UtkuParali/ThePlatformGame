using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Box : MonoBehaviour
{
    private int counter=0;
    public Sprite brokenBox;
    private SpriteRenderer boxSR;
    private Transform boxTransform;

    private void Start()
    {
        boxSR = GetComponent<SpriteRenderer>();
        boxTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Bullet" && counter==0)
        {
            boxSR.sprite= brokenBox;
            counter++;
            boxTransform.DOShakeRotation(0.5f,5);
        }
        else if(other.tag=="Bullet" && counter==1)
        {
            boxSR.DOFade(0, 1);
            Destroy(gameObject);
        }
    }
}
