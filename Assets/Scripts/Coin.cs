using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private UImanager _uiManager;

    [SerializeField]
    private AudioClip _coinCollectSound;

    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Player _player = other.GetComponent<Player>();
                if (_player != null)
                {
                    _player._isCoinCollected = true;
                    _uiManager.CoinUpdate();
                    AudioSource.PlayClipAtPoint(_coinCollectSound, Camera.main.transform.position, 1f);
                    Destroy(this.gameObject);
                }
                
            }
        }
    }

}
