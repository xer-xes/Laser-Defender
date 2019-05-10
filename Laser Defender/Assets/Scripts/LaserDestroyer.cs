﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
        }
    }
}
