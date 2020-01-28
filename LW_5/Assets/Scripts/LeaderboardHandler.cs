using UnityEngine;
using UnityEngine.UI;

public class LeaderboardHandler : MonoBehaviour {
    [SerializeField]
    private Text[] nameArray, scoreArray;

    private void Start() {
        for (int i = 0; i < 5; ++i) {
            nameArray[i].text = PlayerPrefs.GetString("top" + (i + 1) + ".name");
            scoreArray[i].text =
                PlayerPrefs.GetFloat("top" + (i + 1) + ".score").ToString();
        }
    }
}
