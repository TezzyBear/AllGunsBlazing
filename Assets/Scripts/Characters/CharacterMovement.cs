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
    private float maxXPos, maxYPos, minXPos, minYPos;

    void Awake()
    {
        maxXPos = 0.0f;
        maxYPos = 5.0f;
        minXPos = -6.0f;
        minYPos = -5.0f;
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
            transform.GetChild(2).gameObject.SetActive(false);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += speed * Time.deltaTime;
                if (position.y > maxYPos)
                {
                    position.y = maxYPos;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= speed * Time.deltaTime;
                if (position.y < minYPos)
                {
                    position.y = minYPos;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += speed * Time.deltaTime;
                if (position.x > maxXPos)
                {
                    position.x = maxXPos;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x -= speed * Time.deltaTime;
                if (position.x < minXPos)
                {
                    position.x = minXPos;
                }
            }
            transform.position = position;
            
        }
        else
        {
            GetComponent<Renderer>().material.color = startColor;
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
