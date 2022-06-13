using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int _powerupID;         //if 0 -> Triple Shot if 1 -> Speed if 2 -> Shield

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.setTripleShotActive();
                        break;
                    case 1:
                        player.setSpeedBoostActive();
                        break;
                    case 2:
                        player.setShieldActive();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }

            }
            Destroy(this.gameObject);

        }
    }

    
}
