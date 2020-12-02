using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /*          EnemyLvl    EnemyType   EnemyHealth  EnemyArmorHealth   EnemyQuantity   EnemyMovementSpeed TimeOffset(seconds) SpawnRate
     * WAVE 1
     * WAVE 2
     * ...
     */
    private float[,] waveInfo1 = {{0, 4, 100,   0, 60, 0.8f,  4, 1.2f},
                                  {0, 1, 100,  50, 44, 0.8f,  0,   1f},
                                  {0, 0, 100, 100, 44, 0.7f, 10,   1f},
                                  {0, 2, 100, 200, 42, 0.8f,  0, 1.5f},
                                  {0, 3,  50, 200, 48,   1f,  0,    1f}};

    private float[,] waveInfo2 = {{2, 1, 200,  50, 60, 0.4f,  40, 1.8f},
                                  {2, 0, 200, 100, 44, 0.3f,  40, 1.5f},
                                  {2, 2, 120, 200, 44, 0.3f,  60, 1.5f},
                                  {2, 3, 200, 100, 42, 0.8f,  50,   2f},
                                  {2, 2, 1000, 1000, 10, 0.4f,  80, 10f}};

    private float[,] waveInfo3 = {{1, 1, 150,  50, 60, 0.6f, 10, 1.2f},
                                  {1, 0, 150, 100, 44, 0.5f,  5,   1f},
                                  {1, 0, 150, 100, 44, 0.5f,  5,   1f},
                                  {1, 2,  80, 200, 42, 0.5f,  0, 1.2f},
                                  {1, 2, 100, 200, 48, 0.6f,  5, 1.5f}};

    private float[,] waveInfo4 = {{0, 1, 100,  50, 60, 0.8f, 50, 1.6f},
                                  {0, 0, 100, 100, 44, 0.7f, 50, 1.4f},
                                  {0, 2,  80, 200, 44, 0.7f, 50, 1.4f},
                                  {0, 3,  50, 100, 42,   1f, 50, 0.8f},
                                  {0, 2,  80, 200, 48, 0.8f, 50,   1f}};

    private float[,] waveInfo5 = {{2, 1, 200,  50, 60, 0.4f,  0,   2f},
                                  {2, 0, 200, 100, 44, 0.3f,  5, 1.5f},
                                  {2, 2, 120, 200, 44, 0.3f,  0, 1.5f},
                                  {2, 3, 150, 100, 42, 0.8f, 10, 1.5f},
                                  {2, 2, 1000, 1000, 10, 0.4f, 120, 10f}};

    private float[,] waveInfo6 = {{1, 4, 150,   0, 60, 0.6f, 50, 1.4f},
                                  {1, 1, 150,  50, 44, 0.6f, 50, 2.2f},
                                  {1, 0, 150, 100, 44, 0.5f, 50, 2.2f},
                                  {1, 2,  80, 200, 42, 0.6f, 10, 2.3f},
                                  {1, 3,  50, 100, 48, 0.8f, 0,   1f}};

    private GameObject spawn1,
                       spawn2,
                       spawn3,
                       spawn4,
                       spawn5,
                       spawn6;

    public enum State
    {
        Wait,
        Wave
    }

    public static GameController instance;
    [SerializeField]
    private List<GameObject> characterList;
    [HideInInspector]
    public int numberOfCharacters;
    [SerializeField]
    private List<GameObject> coolDownTimerList;
    [SerializeField]
    private GameObject spawnObject;
    private int selectedCharacter;
    private Color startColor;
    private float elapsedTime;
    private int maxWaves = 5;
    private int currentEnemies;
    private int currentWave;
    private float nextWaveTime;
    private State currentState;
    private bool destroying;

    public int towerHitPoints = 500;
    public float waitTime = 10;
    private float killPoints = 0;
    [HideInInspector]
    public int abilityCoolDownObjsSpawned;

    public GameObject panelWin;
    public GameObject panelLoose;
    public GameObject canvas;
    public GameObject characterFrame;

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
            characterList[i].transform.position = new Vector3(characterList[i].transform.position.x - 3.0f, characterList[i].transform.position.y + i * -3.5f + 3.5f, characterList[i].transform.position.z);
            //startColor[i] = characterList[i].GetComponent<Renderer>().material.color;
            coolDownTimerList[i].GetComponent<AbilityCooldownController>().bindedCharacter = characterList[i];
        }
        selectedCharacter = 0;
        selectCharacter(0);

        spawn1 = Instantiate(spawnObject, transform.position + new Vector3(8.47f, 3.18f, 0f), Quaternion.identity);
        spawn2 = Instantiate(spawnObject, transform.position + new Vector3(8.47f, 3.18f, 0f), Quaternion.identity);
        spawn3 = Instantiate(spawnObject, transform.position + new Vector3(8.47f, 0f, 0f), Quaternion.identity);
        spawn4 = Instantiate(spawnObject, transform.position + new Vector3(8.47f, 0f, 0f), Quaternion.identity);
        spawn5 = Instantiate(spawnObject, transform.position + new Vector3(8.47f, -3.23f, 0f), Quaternion.identity);
        spawn6 = Instantiate(spawnObject, transform.position + new Vector3(8.47f, -3.23f, 0f), Quaternion.identity);

        spawn1.GetComponent<EnemySpawnController>().Create(waveInfo1, canvas, this);
        spawn2.GetComponent<EnemySpawnController>().Create(waveInfo2, canvas, this);
        spawn3.GetComponent<EnemySpawnController>().Create(waveInfo3, canvas, this);
        spawn4.GetComponent<EnemySpawnController>().Create(waveInfo4, canvas, this);
        spawn5.GetComponent<EnemySpawnController>().Create(waveInfo5, canvas, this);
        spawn6.GetComponent<EnemySpawnController>().Create(waveInfo6, canvas, this);


        currentEnemies = 0;
        currentWave = -1;
        currentState = State.Wait;
        nextWaveTime = Time.time + waitTime;
        destroying = false;

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


        switch (currentState)
        {
            case State.Wait:
                UpdateWaitState();
                break;
            case State.Wave:
                UpdateWaveState();
                break;
        }
    }

    void UpdateWaitState()
    {
        if(Time.time > nextWaveTime)
        {
            EnterWaveState();
        }
    }

    void UpdateWaveState()
    {
        
        if (currentEnemies == 0)
        {
            EnterWaitState();
        }
    }

    void EnterWaitState()
    {
        currentState = State.Wait;
        nextWaveTime = Time.time + waitTime;
    }

    void EnterWaveState()
    {
        currentWave++;
        if (currentWave == maxWaves)
        {
            youWin();
            return;
        }
        currentState = State.Wave;

        Debug.Log("Wave");
        Debug.Log(currentWave);

        currentEnemies = (int)waveInfo1[currentWave,4] + 
                        (int)waveInfo2[currentWave, 4] + 
                        (int)waveInfo3[currentWave, 4] +
                        (int)waveInfo4[currentWave, 4] +
                        (int)waveInfo5[currentWave, 4] +
                        (int)waveInfo6[currentWave, 4];
        spawn1.SendMessage("EnterWaveState", currentWave);
        spawn2.SendMessage("EnterWaveState", currentWave);
        spawn3.SendMessage("EnterWaveState", currentWave);
        spawn4.SendMessage("EnterWaveState", currentWave);
        spawn5.SendMessage("EnterWaveState", currentWave);
        spawn6.SendMessage("EnterWaveState", currentWave);
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
                characterFrame.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                characterList[i].GetComponent<CharacterMovement>().isSelected = false;
                characterFrame.transform.GetChild(i).gameObject.SetActive(false);
            }
            selectedCharacter = position;
        }
    }

    public void DestroyEnemy()
    {
        while (true)
        {
            if (!destroying)
            {
                destroying = true;
                currentEnemies--;
                destroying = false;
                break;
            }
        }
        
    }


    public void youLoose() {
        panelLoose.SetActive(true);
    }

    void youWin() {
        panelWin.SetActive(true);
    }
}
