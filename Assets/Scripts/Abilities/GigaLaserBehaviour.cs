using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaLaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private float fadeAwayTime = 3.0f;
    private float fadeAwayTimer;
    // Start is called before the first frame update
    void Start()
    {
        fadeAwayTimer = fadeAwayTime;
    }

    // Update is called once per frame
    void Update()
    {
        fadeAwayTimer -= Time.deltaTime;
        if (fadeAwayTimer <= fadeAwayTime && fadeAwayTimer >= fadeAwayTime / 3 * 2)
        {
            transform.localScale = new Vector3(5.0f, 20.0f, 0.0f);
        }
        else if (fadeAwayTimer <= fadeAwayTime / 3 * 2 && fadeAwayTimer >= fadeAwayTime / 3 * 1)
        {
            transform.localScale = new Vector3(3.0f, 20.0f, 0.0f);
            GetComponent<Collider2D>().enabled = false;
        }
        else if (fadeAwayTimer <= fadeAwayTime / 3 * 1  && fadeAwayTimer >= 0)
        {
            transform.localScale = new Vector3(1.0f, 20.0f, 0.0f);
        }
        else if (fadeAwayTimer <= fadeAwayTime) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<AliveController>().Damage(150);
        }
    }
}
