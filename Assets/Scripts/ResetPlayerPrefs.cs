using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("firstTime")) return;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("firstTime", 1);
    }
}
