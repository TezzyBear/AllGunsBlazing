using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController instance;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    public float rotationMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rotationMultiplier = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    BigShake();
        //}
    }

    private void LateUpdate()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1.0f, 1.0f) * shakePower;
            float yAmount = Random.Range(-1.0f, 1.0f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0.0f);
            shakePower = Mathf.MoveTowards(shakePower, 0.0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0.0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, shakeRotation * Random.Range(-1.0f, 1.0f));
    }

    public void StartShake(float time, float power)
    {
        shakeTimeRemaining = time;
        shakePower = power;
        shakeFadeTime = power / time;
        shakeRotation = power * rotationMultiplier;
    }

    public void LittleShake()
    {
        shakeTimeRemaining = 0.5f;
        shakePower = 0.2f;
        shakeFadeTime = 0.2f / 0.5f;
        shakeRotation = 0.2f * rotationMultiplier;
    }

    public void BigShake()
    {
        shakeTimeRemaining = 1.0f;
        shakePower = 0.65f;
        shakeFadeTime = 0.65f / 1.0f;
        shakeRotation = 0.65f * rotationMultiplier;
    }
}
