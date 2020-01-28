using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
    [SerializeField]
    private InputField inputName;

    public void Enter() {
        if (inputName.text.Length == 0)
            return;
        float score = PlayerPrefs.GetFloat("score");
        for (int i = 1; i <= 5; ++i)
            if (score > PlayerPrefs.GetFloat("top" + i + ".score")) {
                LeaderboardMove(i);
                PlayerPrefs.SetString("top" + i + ".name", inputName.text);
                PlayerPrefs.SetFloat("top" + i + ".score", score);
                break;
            }
        SceneManager.LoadScene("Leaderboard");
    }

    private void LeaderboardMove(int index) {
        for (int i = 5; i > index; --i) {
            string topName = PlayerPrefs.GetString("top" + (i - 1) + ".name");
            float topScore = PlayerPrefs.GetFloat("top" + (i - 1) + ".score");
            PlayerPrefs.SetString("top" + i + ".name", topName);
            PlayerPrefs.SetFloat("top" + i + ".score", topScore);
        }
    }
}
