using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.OpenSkill();
            AudioManager.Instance.PlaySFX("CollectSFX");
            gameObject.SetActive(false);
        }
    }
}
