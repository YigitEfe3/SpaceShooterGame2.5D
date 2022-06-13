using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private float _speed = 5f;
    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _playerShield;
    private UIManager _uiManager;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();

        
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
        
    void FireLaser()
    {
        Vector3 direction = new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z);
        Vector3 directionTripleShot = new Vector3(transform.position.x - 1.61f , transform.position.y, transform.position.z);

        _canFire = Time.time + _fireRate;

        
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, directionTripleShot, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, direction, Quaternion.identity);
                                                                        
        }                                               //alternative without the direction variable
                                                        //Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }
    

    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //FIRST OPTION
        //new Vector3.right
        //transform.Translate(new Vector3(1, 0, 0) * _speed * horizontalInput * Time.deltaTime);
        //transform.Translate(new Vector3(0, 1, 0) * _speed * verticalInput * Time.deltaTime);
        //new Vector3.left

        //SECOND OPTION
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //THIRD OPTION
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= -1.3f)
        {
            transform.position = new Vector3(transform.position.x, -1.3f, 0);
        }
        else if (transform.position.y < -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f, 0);
        }
        //OPTION 2 FOR ABOVE
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y , -4.3f , 0), 0);

        if (transform.position.x >= 11.26f)
        {
            transform.position = new Vector3(-11.26f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.26f)
        {
            transform.position = new Vector3(11.26f, transform.position.y, 0);
        }
    }

    public void Damage()
    {

        if (_isShieldActive)
        {
            _isShieldActive = false;
            _playerShield.SetActive(false);
            return;
        }
        else
        {
            _lives--;

            _uiManager.updateLives(_lives);

            if (_lives < 1)
            {
                Destroy(this.gameObject);
                _spawnManager.onPlayerDeath();
            }
        }
        
            
    }

    public void setShieldActive()
    {
        _isShieldActive = true;
        //turn on the shield
        _playerShield.SetActive(true);
    }


    public void setTripleShotActive()
    {
        _isTripleShotActive = true;

        StartCoroutine(tripleShotPowerDownRoutine());

    }

    IEnumerator tripleShotPowerDownRoutine()
    {
        while (5 < 6)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;

            //Burdan sonra loop'a bidaha giriyodu, bu da bir sonraki power'up alýndýðýnda
            //sürenin kýsalmasýna sebep oluyodu, break koyunca çözüldü.
            break;

        }
    }

    public void setSpeedBoostActive()
    {
        _speed = _speed * _speedMultiplier;

        StartCoroutine(speedBoostPowerDownRoutine());

    }

    IEnumerator speedBoostPowerDownRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            _speed = _speed / _speedMultiplier;

            break;

        }
    }

    //method for updating score
    public void updateScore(int points)
    {
        _score = _score + points;
        _uiManager.addScore(_score);
    }


}
