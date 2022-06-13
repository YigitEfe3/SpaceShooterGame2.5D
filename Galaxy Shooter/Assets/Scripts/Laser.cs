using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;

    void Update()
    {
        Vector3 direction = new Vector3(0,  _laserSpeed * Time.deltaTime, 0);
        transform.Translate(direction);

        if(transform.position.y > 7)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
