using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private Image exitMenu;
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private SpriteRenderer[] backgrounds;
    private float longDay = 30f;
    private int index = 0;
    private bool hasChange;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            exitMenu.gameObject.SetActive(true);
        }

        if (!bird.IsDead && !hasChange)
        {
            StartCoroutine(ChangeBackground());
        }
    }

    private IEnumerator ChangeBackground()
    {
        switch (index)
        {
            case 0: // Morning
                Debug.Log("Case 0");
                
                hasChange = true;
                yield return new WaitForSeconds(longDay);
                index = 1; // Set to night
                
                Debug.Log("done waiting_Case 0");
                // Change sprites
                for (int i = backgrounds.Length-1; i >= 0; i--)
                {
                    backgrounds[i].sprite = backgroundSprites[index];
                    yield return new WaitForSeconds(1.5f);
                }
                hasChange = false;
                break;
            case 1: // Night
                Debug.Log("Case 1");
                
                hasChange = true;
                yield return new WaitForSeconds(longDay);
                index = 0; // Set to night
                
                Debug.Log("done waiting_Case 1");
                // Change sprites
                for (int i = backgrounds.Length-1; i >= 0; i--)
                {
                    backgrounds[i].sprite = backgroundSprites[index];
                    yield return new WaitForSeconds(1.5f);
                }
                hasChange = false;
                break;
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        Time.timeScale = 1f;
        exitMenu.gameObject.SetActive(false);
    }
}
