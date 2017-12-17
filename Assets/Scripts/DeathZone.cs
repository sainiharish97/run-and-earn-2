using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            SceneManager.LoadScene("main");
        }
    }
}
