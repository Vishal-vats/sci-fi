using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookVertical : MonoBehaviour
{
    [SerializeField]
    private float senstivityY = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 tempangle = transform.localEulerAngles;
        tempangle.x += mouseY * senstivityY;
        
        transform.localEulerAngles = tempangle;


    }
}
