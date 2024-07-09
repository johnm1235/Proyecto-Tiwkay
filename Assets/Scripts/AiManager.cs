using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager instance;
    public Transform player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

   
