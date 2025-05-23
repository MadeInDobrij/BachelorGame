using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D colliosion)
    {
        if (colliosion.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
    
    void Update()
    {
        
    }
}
