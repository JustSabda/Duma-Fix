using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool isTaken;

    private void Start()
    {
        isTaken = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isTaken)
            {
                GameManager.Instance.Objective++;
                isTaken = true;
            }
            this.gameObject.SetActive(false);
        }
    }
}
