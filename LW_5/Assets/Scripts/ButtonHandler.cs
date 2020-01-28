using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler,
    IPointerEnterHandler, IPointerExitHandler {
    private Text button;
    [SerializeField]
    private Color newColor = new Color(2F / 15F, 139F / 255F, 2F / 15F);
    private Color startColor = new Color(139F / 255F, 0, 0);

    private void Start() {
        button = gameObject.GetComponent<Text>();
        startColor = button.color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        button.color = newColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        button.color = startColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        switch(button.gameObject.name) {
            case "Back":
                SceneManager.LoadScene("Menu");
                break;
            case "Exit":
                Application.Quit();
                break;
            case "First":
                PlayerPrefs.SetInt("level", 0);
                SceneManager.LoadScene("Main");
                break;
            case "Leaderboard":
                SceneManager.LoadScene("Leaderboard");
                break;
            case "Second":
                PlayerPrefs.SetInt("level", 1);
                SceneManager.LoadScene("Main");
                break;
            case "SelectLevel":
                SceneManager.LoadScene("SelectLevel");
                break;
            case "Start":
                SceneManager.LoadScene("Main");
                break;
            case "Third":
                PlayerPrefs.SetInt("level", 2);
                SceneManager.LoadScene("Main");
                break;
        }
    }
}
