using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyedCrate;

    public void destroyCrate()
    {
        Instantiate(_destroyedCrate, transform.position, transform.rotation);
        Destroy(this.gameObject, 0.01f);
    }

}
