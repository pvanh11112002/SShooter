using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 5f;

    private float spriteHeight;
    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        transform.Translate(Vector3.down * scrollingSpeed * Time.deltaTime);
        if(transform.position.y < startPos.y - spriteHeight)
        {
            transform .position = startPos;
        }    
    }
}
