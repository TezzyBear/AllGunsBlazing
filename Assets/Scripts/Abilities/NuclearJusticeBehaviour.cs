using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearJusticeBehaviour : MonoBehaviour
{
    [SerializeField]
    private float fadeAwayTime = 3.0f;
    private float fadeAwayTimer;
    private float initialAlpha;

    // Start is called before the first frame update
    void Start()
    {
        fadeAwayTimer = fadeAwayTime;
        initialAlpha = GetComponent<SpriteRenderer>().color.a;

    }

    // Update is called once per frame
    void Update()
    {
        fadeAwayTimer -= Time.deltaTime;
        if (fadeAwayTimer <= 0) {
            Destroy(this.gameObject);
        }

        Color thisColor = GetComponent<SpriteRenderer>().color;

        GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, initialAlpha * (fadeAwayTimer / fadeAwayTime));
    }
}

