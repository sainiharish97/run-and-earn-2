﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoadScene1 : MonoBehaviour
{
    public void Change(string Scenes)
    {
        SceneManager.LoadScene(Scenes);
    }
}
