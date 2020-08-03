using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    [SerializeField] private Text highScoreNum;

    private void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
            highScoreNum.text = PlayerPrefs.GetInt("highScore").ToString();
        else
            gameObject.SetActive(false);
    }
}
