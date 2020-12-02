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
    [SerializeField]
    private float period = 5f;
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
    }

    void SetWeather(Weather w)
    {

        switch (w)
        {
            case Weather.ELECTRIC_STORM:
                weatherText.text = "TORMENTA ELÉCTRICA";
                weatherText.color = new Color(0.7333f, 0.7882f, 0.2039f, 1.0f);
                break;
            case Weather.DESERT:
                weatherText.text = "DESÉRTICO";
                weatherText.color = new Color(0.8784f, 0.6039f, 0.2706f, 1.0f);
                break;
            case Weather.HEAVY_WIND:
                windforce = Random.Range(-1f, 1f);
                weatherText.text = "VIENTO PESADO\nFUERZA:" + ((int)Mathf.Round(windforce*100)).ToString() + "%";
                weatherText.color = new Color(0.4549f, 0.4784f, 0.5686f, 1.0f);
                break;
            case Weather.RAIN:
                weatherText.text = "LLUVIA";
                weatherText.color = new Color(0.1922f, 0.5882f, 0.6f, 1.0f);
                break;
            case Weather.BLIZZARD:
                weatherText.text = "VENTISCA";
                weatherText.color = new Color(0.0862f, 0.6510f, 0.7882f, 1.0f);
                break;
            default:
                break;
        }
        weather = w;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] fireArms = GameObject.FindGameObjectsWithTag("Tool");
        

        foreach(GameObject bullet in bullets)
        {
            bullet.SendMessage("setWeatherEffect", new WeatherData(weather, windforce));
        }
        foreach(GameObject fireArm in fireArms)
        {
            fireArm.SendMessage("setWeather", new WeatherData(weather, windforce));
        }
    }
}
