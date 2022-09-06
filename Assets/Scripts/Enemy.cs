using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Slider slider;
    public float health;
    public float damage;
    bool colliderBusy=false;
    bool dead = false;
    SpriteRenderer spriteRenderer;
    Transform transform;
    public Transform gem;
    


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        slider.value = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player" && !colliderBusy)
        {
            colliderBusy = true;
            other.GetComponent<Player>().GetDamage(damage);
        }
        else if(other.tag=="Bullet")
        {
            GetDamage(other.GetComponent<BulletManager>().damage);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            colliderBusy = false;
        }
    }

    public void GetDamage(float damage)
    {
        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if (health <= 0)
        {
            float localDuration = 1f;
            dead = true;
            spriteRenderer.DOFade(0, localDuration);
            Destroy(gameObject);
            Transform tempGem;
            tempGem = Instantiate(gem, transform.position+ new Vector3(0f,0.75f,0f), Quaternion.identity);
            
        }
    }



}
