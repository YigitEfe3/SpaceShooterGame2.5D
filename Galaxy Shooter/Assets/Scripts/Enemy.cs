using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _EnemySpeed = 4f;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10f, 10f) , 6, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _EnemySpeed * Time.deltaTime);

        if(transform.position.y < -7f)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 7f, transform.position.z);
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
                player.Damage();

            Destroy(this.gameObject);
        }

        else if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            
            if(_player != null)
            {
                _player.updateScore(10);    //kazandýðýn skoru randomlayabilir veya düþmana göre farklý puan verebilirsin.
            }
            

            Destroy(other.gameObject);
        }
            
            
    }
}
