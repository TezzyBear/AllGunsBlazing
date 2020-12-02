using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private BulletType.Type type;
    private float speed;
    private float startSpeed;
    private float travelDistance;
    private float startTravelDistance;
    private Vector3 initialPosition;

    private void Awake()
    {
        travelDistance = 8.0f; //Medium distance
        startTravelDistance = travelDistance;
        speed = 4.0f;
        startSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);
        if (distanceTraveled >= travelDistance)
        {
            Destroy(this.gameObject);
        }
    }
    public void Create(BulletType.Type t, float travelDistance, float speed, WeatherController.WeatherData wd)
    {
        this.travelDistance = travelDistance;
        startTravelDistance = travelDistance;
        this.speed = speed;
        startSpeed = speed;
        this.type = t;
        setWeatherEffect(wd);
    }

    public void setWeatherEffect(WeatherController.WeatherData wd)
    {
        WeatherController.Weather w = wd.weather;
        float wf = wd.windforce;
        travelDistance = startTravelDistance;
        speed = startSpeed;
        switch (w)
        {
            case WeatherController.Weather.DESERT:
                if (type != BulletType.Type.INCENDIARY)
                    travelDistance = (int)Mathf.Round(travelDistance * 0.6f);
                break;
            case WeatherController.Weather.HEAVY_WIND:
                switch (type)
                {
                    case BulletType.Type.PIERCING: break;
                    case BulletType.Type.BLEEDING: speed = (int)Mathf.Round(speed * (1 + (0.4f * wf))); break;
                    case BulletType.Type.EXPLOSIVE: speed = (int)Mathf.Round(speed * (1 + (0.2f * wf))); break;
                    case BulletType.Type.BLUNDERING: speed = (int)Mathf.Round(speed * (1 + (0.2f * wf))); break;
                    case BulletType.Type.INCENDIARY: speed = (int)Mathf.Round(speed * (1 + (0.6f * wf))); break;
                    case BulletType.Type.FREEZING: speed = (int)Mathf.Round(speed * (1 + (0.2f * wf))); break;
                }
                break;
            default: break;
        }
    }
}
