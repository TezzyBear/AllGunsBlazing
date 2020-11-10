using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private List<GameObject> waveList;
    [SerializeField]
    private List<GameObject> characterList;
    [HideInInspector]
    public int numberOfCharacters;
    [SerializeField]
    private List<GameObject> coolDownTimerList;
    private int selectedCharacter;
    private Color startColor;
    private float elapsedTime;

    public float towerHitPoints = 500;
    private float killPoints = 0;
    [HideInInspector]
    public int abilityCoolDownObjsSpawned;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return; //Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        abilityCoolDownObjsSpawned = 0;
        numberOfCharacters = characterList.Count;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            characterList[i] = Instantiate(characterList[i], transform.position, Quaternion.identity);
            characterList[i].transform.position = new Vector3(characterList[i].transform.position.x -3.0f, characterList[i].transform.position.y+i*3.5f-3.5f, characterList[i].transform.position.z);
            //startColor[i] = characterList[i].GetComponent<Renderer>().material.color;
            coolDownTimerList[i].GetComponent<AbilityCooldownController>().bindedCharacter = characterList[i];
        }
        selectedCharacter = 0;
        selectCharacter(0);
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            selectCharacter(0);
        }
        if (Input.GetKeyDown("2"))
        {
            selectCharacter(1);
        }
        if (Input.GetKeyDown("3"))
        {
            selectCharacter(2);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            nextCharacter();
        }
    }

    void nextCharacter()
    {
        selectedCharacter = selectedCharacter + 1;
        if (selectedCharacter > 2)
        {
            selectedCharacter = 0;
            selectCharacter(selectedCharacter);
        }
        else
        {
            selectCharacter(selectedCharacter);
        }
    }

    void selectCharacter(int position)
    {
        for(int i = 0; i < characterList.Count; i++)
        {
            if(position == i)
            {
                characterList[i].GetComponent<CharacterMovement>().isSelected = true;
            }
            else
            {
                characterList[i].GetComponent<CharacterMovement>().isSelected = false;
            }
            selectedCharacter = position;
        }
    }

    void youLoose() { 
    
    }

    void youWin() { 
    
    }
}
