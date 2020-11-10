using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    private Vector3 position;
    public bool isSelected;
    private Color startColor;
    

    void Awake()
    {
        isSelected = false;
    }

    void Start()
    {
        speed = 3.0f;
        position = transform.position;
        startColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, 0, translation);
        if(isSelected) {
            GetComponent<Renderer>().material.color = Color.cyan;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x -= speed * Time.deltaTime;
            }
            transform.position = position;
        }
        else
        {
            GetComponent<Renderer>().material.color = startColor;
        }
    }
}
