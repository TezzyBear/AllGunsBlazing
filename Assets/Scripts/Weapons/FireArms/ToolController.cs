using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    [SerializeField]
    private GameObject toolQ;
    [SerializeField]
    private GameObject toolW;
    [SerializeField]
    private GameObject toolE;

    // Start is called before the first frame update
    void Start()
    {
        GameObject toolQinstance = Instantiate(toolQ, transform.position, Quaternion.identity);
        GameObject toolWinstance = Instantiate(toolW, transform.position, Quaternion.identity);
        toolWinstance.SetActive(false);
        GameObject toolEinstance = Instantiate(toolE, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("q"))
        {
            ChangeWeapon(weaponQ)
        }
        if (Input.GetKeyDown("w"))
        {
            ChangeWeapon()
        }
        if (Input.GetKeyDown("e"))
        {
            ChangeWeapon()
        }*/
    }

    void ChangeWeapon(string weapon) { 
        
    }
}
