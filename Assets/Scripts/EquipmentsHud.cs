using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsHud : MonoBehaviour
{
    public bool isUpgraded;
    public GameObject Upgraded;
    public GameObject NotUpgraded;

    private void Start()
    {
        UpdateImage();
    }
    public void UpdateImage()
    {
        Upgraded?.SetActive(isUpgraded);
        NotUpgraded?.SetActive(!isUpgraded);
    }
}
