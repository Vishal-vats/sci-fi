using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _currentAmmoText;

    [SerializeField]
    private Text _maxAmmoCapacityText;

    [SerializeField]
    private GameObject _coin;

    public void currentAmmoUpdate(int ammoCount)
    {
        _currentAmmoText.text = ammoCount + " /";
    }

    public void maxAmmoCapacityUpdate(int maxCount)
    {
        _maxAmmoCapacityText.text = maxCount.ToString();
    }

    public void CoinUpdate()
    {
        _coin.SetActive(true);
    }

    public void deActivateCoin()
    {
        _coin.SetActive(false);
    }
}
