using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _lifeSprites;  //Array with 4 differen life status (3, 2, 1, 0)
    [SerializeField]
    private Image _livesImage;      //The life image we will display on screen
    [SerializeField]
    private Text _gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: "+ 0;
        _gameOverText.gameObject.SetActive(false);
    }

    public void addScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
                                    //playerScore.ToString(); ayný þey
    }

    public void updateLives(int currentLives)
    {
        _livesImage.sprite = _lifeSprites[currentLives];

        if(currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
