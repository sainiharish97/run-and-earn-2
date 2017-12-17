using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnmainmenu : MonoBehaviour {

    public string scene;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("player"))
        {

            SceneManager.LoadScene(scene);

        }

    }
}
