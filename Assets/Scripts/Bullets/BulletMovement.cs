using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed;
    private float type;
    private float travelDistance;
    private Vector3 initialPosition;

    private void Awake()
    {
        travelDistance = 8.0f; //Medium distance
        speed = 4.0f;
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
    public void setTravelDistance(float travelDistance) {
        this.travelDistance = travelDistance;
    }
    public void setSpeed(float speed) {
        this.speed = speed;
    }
}
