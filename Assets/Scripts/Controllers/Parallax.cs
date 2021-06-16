using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField]
    private GameObject camPlayer;
    private float lenght, startPos;

    [SerializeField]
    private float speedParallax;

    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    void FixedUpdate()
    {
        float temp = (camPlayer.transform.position.x * (1 - speedParallax));
        float dist = (camPlayer.transform.position.x * speedParallax);

        transform.position = new Vector3(startPos + dist, 0f, 0f);

        if(temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if(temp < startPos - lenght)
        {
            startPos -= lenght;
        }

    }
}
