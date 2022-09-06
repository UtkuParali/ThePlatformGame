using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float damage = 20;
    public float lifeTime = 4f;
    Transform bullet;

    // Start is called before the first frame update
    void Start()
    {

        bullet = GetComponent<Transform>();
        
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
