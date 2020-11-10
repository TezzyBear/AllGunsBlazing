using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaseHP : MonoBehaviour
{
    private Transform bar;
    private Vector3 originalSize;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        originalSize = bar.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(originalSize.x < 1)
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                setSize(originalSize.x + 0.1f);
            }
            originalSize = bar.localScale;
        }
        if (originalSize.x > 0)
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                setSize(originalSize.x - 0.1f);
            }
            originalSize = bar.localScale;
        }
    }

    void setSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1.0f, 0.0f);
    }
}
