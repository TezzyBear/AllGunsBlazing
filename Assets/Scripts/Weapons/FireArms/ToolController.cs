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
        //Deactivating instances before usage
        toolQ = Instantiate(toolQ, transform.position, Quaternion.identity);
        toolQ.transform.parent = this.transform;
        toolQ.SetActive(false);
        toolW = Instantiate(toolW, transform.position, Quaternion.identity);
        toolW.transform.parent = this.transform;
        toolW.SetActive(false);
        toolE = Instantiate(toolE, transform.position, Quaternion.identity);
        toolE.transform.parent = this.transform;
        toolE.SetActive(false);

        takeOutTool('q');
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown("q"))
        {
            takeOutTool('q');
        }
        if (Input.GetKeyDown("w"))
        {
            takeOutTool('w');
        }
        if (Input.GetKeyDown("e"))
        {
            takeOutTool('e');
        }
    }

    void takeOutTool(char letter) {
        if (letter == 'q')
        {
            toolQ.SetActive(true);
            toolW.SetActive(false);
            toolE.SetActive(false);
        }
        else if (letter == 'w')
        {
            toolQ.SetActive(false);
            toolW.SetActive(true);
            toolE.SetActive(false);
        }
        else if (letter == 'e') {
            toolQ.SetActive(false);
            toolW.SetActive(false);
            toolE.SetActive(true);
        }
    }
}
