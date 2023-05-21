using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkManShop : MonoBehaviour
{
    private AudioSource _audioSource;
    private UImanager _uiManager;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Player _player = other.GetComponent<Player>();

                if (_player != null)
                {
                    if (_player._isCoinCollected == true)
                    {
                        _audioSource.Play();
                        _uiManager.deActivateCoin();
                        _player._isCoinCollected = false;
                        _player.enableWeapon();
                    }
                    else
                    {
                        Debug.Log("You don't have coin");
                    }
                }
                
            }
        }
    }
}
