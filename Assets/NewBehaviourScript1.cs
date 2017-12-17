using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {


    [SerializeField]
    GameObject mExplosionPrefab;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("BusterBullet"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);


        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.GetComponent<MegaMan>().TakeDamage(3);
        }
    }
}



