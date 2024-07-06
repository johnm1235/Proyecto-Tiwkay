using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioCamara : MonoBehaviour
{
    public GameObject[] camaras;


    // Start is called before the first frame update
    void Start()
    {
        camaras[0].gameObject.SetActive(true);
        camaras[1].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            camaras[0].gameObject.SetActive(true);
            camaras[1].gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            camaras[0].gameObject.SetActive(false);
            camaras[1].gameObject.SetActive(true);
        }
    }
}
