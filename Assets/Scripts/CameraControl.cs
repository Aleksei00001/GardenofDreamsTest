using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject targetMove;

    private void Update()
    {
        if (targetMove != null)
        {
            transform.position = new Vector3(targetMove.transform.position.x, targetMove.transform.position.y, transform.position.z);
        }
    }
}
