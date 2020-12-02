using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType : MonoBehaviour
{   
    public enum Type {
        PIERCING,
        BLEEDING,
        EXPLOSIVE,
        BLUNDERING,
        INCENDIARY,
        FREEZING
    }

    
    public int
        damage, startDamage;

    private SpriteRenderer spriteRenderer;

    public Type type;
    [SerializeField]
    private Sprite[] sprites;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Create(int dmg, Type t, WeatherController.WeatherData wd)
    {
        type = t;
        spriteRenderer.sprite = sprites[(int)t];
        damage = dmg;
        startDamage = damage;

        setWeatherEffect(wd);
    }
    public void setWeatherEffect(WeatherController.WeatherData wd)
    {
        WeatherController.Weather w = wd.weather;
        float wf = wd.windforce;
        damage = startDamage;
        switch (w)
        {
            case WeatherController.Weather.ELECTRIC_STORM:

                if (type == BulletType.Type.FREEZING)
                    damage = (int)Mathf.Round(damage * 1.15f);

                break;
            case WeatherController.Weather.DESERT:

                if (type == BulletType.Type.INCENDIARY)
                    damage = (int)Mathf.Round(damage * 1.1f);
                else if (type == BulletType.Type.FREEZING)
                    damage = (int)Mathf.Round(damage * 0.9f);

                break;
            case WeatherController.Weather.HEAVY_WIND:
                switch (type)
                {
                    case BulletType.Type.PIERCING: break;
                    case BulletType.Type.BLEEDING: damage = (int)Mathf.Round(damage * (1 + (0.4f * wf))); break;
                    case BulletType.Type.EXPLOSIVE: damage = (int)Mathf.Round(damage * (1 + (0.2f * wf))); break;
                    case BulletType.Type.BLUNDERING: damage = (int)Mathf.Round(damage * (1 + (0.2f * wf))); break;
                    case BulletType.Type.INCENDIARY: damage = (int)Mathf.Round(damage * (1 + (0.6f * wf))); break;
                    case BulletType.Type.FREEZING: damage = (int)Mathf.Round(damage * (1 + (0.2f * wf))); break;
                }
                break;
            case WeatherController.Weather.RAIN:
                switch (type)
                {
                    case BulletType.Type.EXPLOSIVE: damage = (int)Mathf.Round(damage * 0.9f); break;
                    case BulletType.Type.INCENDIARY: damage = (int)Mathf.Round(damage * 0.85f); break;
                    default: break;
                }
                break;
            case WeatherController.Weather.BLIZZARD:
                switch (type)
                {

                    case BulletType.Type.BLEEDING: damage = (int)Mathf.Round(damage * 0.9f); break;
                    case BulletType.Type.EXPLOSIVE: damage = (int)Mathf.Round(damage * 0.85f); break;
                    case BulletType.Type.INCENDIARY: damage = (int)Mathf.Round(damage * 0.85f); break;
                    case BulletType.Type.FREEZING: damage = (int)Mathf.Round(damage * 1.1f); break;
                    default: break;
                }
                break;
        }
    }

}
