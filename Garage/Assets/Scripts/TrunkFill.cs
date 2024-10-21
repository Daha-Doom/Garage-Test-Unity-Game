using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkFill : MonoBehaviour
{
    [SerializeField]
    GameObject winText;
    int fruitsCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("fruit"))
            fruitsCount++;

        if (fruitsCount == 3)
        {
            Debug.Log("Цель выполнена!");

            winText.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("fruit"))
            fruitsCount--;
    }
}
