using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherController : MonoBehaviour
{
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
    private float nextWeatherTime = 0.0f;
    private float period = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        weather = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
        SetWeather(weather);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextWeatherTime)
        {
            nextWeatherTime += period;
            weather = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
            SetWeather(weather);
        }
    }

    void SetWeather(Weather weather)
    {
        switch(weather)
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
                weatherText.text = "VIENTO PESADO";
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
    }
}
