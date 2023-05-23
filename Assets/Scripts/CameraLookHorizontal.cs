
using UnityEngine;

public class CameraLookHorizontal : MonoBehaviour
{
    [SerializeField]
    private float senstivityX = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");

        Vector3 tempangle = transform.localEulerAngles;
        tempangle.y += _mouseX * senstivityX;
        transform.localEulerAngles = tempangle;

        
    }
}
