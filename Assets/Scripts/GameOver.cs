using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HwatuDefence
{
    public class GameOver : MonoBehaviour
    {
        public Text roundsText;

        void OnEnable()
        {
            roundsText.text = WaveSpawner._nextWaveCount.ToString();
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Menu()
        {
            Debug.Log("메뉴 화면으로 돌아가기");
            
            LoadingSceneController.Instance.LoadScene("IntroMenu");
        }

    }

}
