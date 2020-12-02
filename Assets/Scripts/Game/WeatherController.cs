using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherController : MonoBehaviour
{

    public struct WeatherData
    {
        public Weather weather;
        public float windforce;
        public WeatherData(Weather w, float wf)
        {
            this.weather = w;
            this.windforce = wf;
        }
    }
    public enum Weather
    {
        ELECTRIC_STORM,
        DESERT,
        HEAVY_WIND,
        RAIN,
        BLIZZARD
    }
    public TextMeshProUGUI weatherText;
    private int fontSize;
    private Weather weather;
    private float windforce = 0;
    private float nextWeatherTime = 0.0f;
    private float nextThunderTime = 0.0f;
    [SerializeField]
    private float period = 5f;
    [SerializeField]
    private Sprite[] bgs;
    [SerializeField]
    private GameObject thunderPref;

    public GameObject bg;
    public GameObject snowGen;
    public GameObject rainGen;
    public GameObject sun;
    // Start is called before the first frame update

    void Start()
    {
        Weather tmp = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
        SetWeather(tmp);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextWeatherTime)
        {
            nextWeatherTime += period;
            Weather tmp = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
            SetWeather(tmp);
        }
        if(weather == Weather.ELECTRIC_STORM)
        {
            if(Time.time > nextThunderTime)
            {
                nextThunderTime += Random.Range(0.5f,2f);
                Instantiate(thunderPref, transform.position, Quaternion.identity);
            }
        }
        
    }

    void SetWeather(Weather w)
    {

        switch (w)
        {
            case Weather.ELECTRIC_STORM:
                weatherText.text = "TORMENTA ELÉCTRICA";
                weatherText.color = new Color(0.7333f, 0.7882f, 0.2039f, 1.0f);
                bg.GetComponent<SpriteRenderer>().sprite = bgs[0];
                rainGen.SetActive(false);
                snowGen.SetActive(false);
                sun.GetComponent<Light>().intensity = 0.6f;
                break;
            case Weather.DESERT:
                weatherText.text = "DESÉRTICO";
                weatherText.color = new Color(0.8784f, 0.6039f, 0.2706f, 1.0f);
                bg.GetComponent<SpriteRenderer>().sprite = bgs[1];
                rainGen.SetActive(false);
                snowGen.SetActive(false);
                sun.GetComponent<Light>().intensity = 1.7f;
                break;
            case Weather.HEAVY_WIND:
                windforce = Random.Range(-1f, 1f);
                weatherText.text = "VIENTO PESADO\nFUERZA:" + ((int)Mathf.Round(windforce*100)).ToString() + "%";
                weatherText.color = new Color(0.4549f, 0.4784f, 0.5686f, 1.0f);
                bg.GetComponent<SpriteRenderer>().sprite = bgs[1];
                rainGen.SetActive(false);
                snowGen.SetActive(false);
                sun.GetComponent<Light>().intensity = 1f;
                break;
            case Weather.RAIN:
                weatherText.text = "LLUVIA";
                weatherText.color = new Color(0.1922f, 0.5882f, 0.6f, 1.0f);
                bg.GetComponent<SpriteRenderer>().sprite = bgs[0];
                rainGen.SetActive(true);
                snowGen.SetActive(false);
                sun.GetComponent<Light>().intensity = 1f;
                break;
            case Weather.BLIZZARD:
                weatherText.text = "VENTISCA";
                weatherText.color = new Color(0.0862f, 0.6510f, 0.7882f, 1.0f);
                bg.GetComponent<SpriteRenderer>().sprite = bgs[2];
                rainGen.SetActive(false);
                snowGen.SetActive(true);
                sun.GetComponent<Light>().intensity = 1.3f;
                break;
            default:
                break;
        }
        weather = w;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] fireArms = GameObject.FindGameObjectsWithTag("Tool");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

        foreach (GameObject bullet in bullets)
        {
            bullet.SendMessage("setWeatherEffect", new WeatherData(weather, windforce));
        }
        foreach(GameObject fireArm in fireArms)
        {
            fireArm.SendMessage("setWeather", new WeatherData(weather, windforce));
        }
        foreach (GameObject enemy in enemies)
        {
            enemy.SendMessage("setWeatherEffect", new WeatherData(weather, windforce));
        }
        foreach (GameObject enemySpawner in enemySpawners)
        {
            enemySpawner.SendMessage("setWeather", new WeatherData(weather, windforce));
        }
    }
}
