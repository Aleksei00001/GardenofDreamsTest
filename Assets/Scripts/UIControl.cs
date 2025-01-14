using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (player == null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
