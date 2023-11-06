using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorPanel : MonoBehaviour
{
    bool isCollided = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isCollided)
        {
            isCollided = true;
            UIManager.Instance.OpenTutor();
        }
    }


}
