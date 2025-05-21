using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerControl : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 offset;
    private Vector3 position;
    private float maxL;
    private float maxR;
    private float maxU;
    private float maxD;
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    private void Start()
    {
        StartCoroutine(SetBoundries());
    }
    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].finger.index == 0)
            {
                Touch myTouch = Touch.activeTouches[0];
                Vector3 touchPos = myTouch.screenPosition;
#if UNITY_EDITOR
                if (touchPos.x == Mathf.Infinity)
                    return;
#endif
                touchPos = mainCam.ScreenToWorldPoint(touchPos);
                if (Touch.activeTouches[0].phase == TouchPhase.Began)
                {
                    offset = touchPos - transform.position;
                }
                if (Touch.activeTouches[0].phase == TouchPhase.Moved || Touch.activeTouches[0].phase == TouchPhase.Stationary)
                {
                    position.x = touchPos.x - offset.x;
                    position.y = touchPos.y - offset.y;
                    position.z = 0;
                    transform.position = position;
                }
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxL, maxR), Mathf.Clamp(transform.position.y, maxD, maxU), 0);
            }          
        }
    }
    private IEnumerator SetBoundries()
    {
        yield return new WaitForSeconds(0.5f);
        mainCam = Camera.main;
        maxL = mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxR = mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
        maxU = mainCam.ViewportToWorldPoint(new Vector2(0f, 0.85f)).y;
        maxD = mainCam.ViewportToWorldPoint(new Vector2(0, 0.15f)).y;
    }    
}
