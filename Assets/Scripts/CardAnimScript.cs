using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class CardAnimScript : MonoBehaviour
    {
        private Transform player;

        private Animator animator;

        public Text text;
        public Text titleText;

        public AudioSource audioSource;

        [Range(0, 1)]
        public byte b;

        void Start()
        {
            player = Camera.main.transform;
            animator = GetComponent<Animator>();
            animator.SetBool("isEnter", false);
            text.color = new Color32(255, 255, 255, 0);
            titleText.color = new Color32(255, 255, 255, 255);
        }

        void OnMouseOver()
        {
            if(player)
            {
                animator.SetBool("isEnter", true);
                text.color = new Color32(255, 255, 255, 255);

                if(Input.GetMouseButtonDown(0))
                {
                    if(b == 0)
                    {
                        animator.SetBool("isEnter", false);
                        text.color = new Color32(255, 255, 255, 0);
                        titleText.color = new Color32(255, 255, 255, 0);
                        GameStart();
                    }
                    else
                    {
                        Debug.Log("종료하기");
                        animator.SetBool("isEnter", false);
                        ApplicationQuit();
                    }
                }
            }
        }

        void OnMouseExit()
        {
            if(player)
            {
                animator.SetBool("isEnter", false);
                text.color = new Color32(255, 255, 255, 0);
            }
        }

        
        public void GameStart()
        {
            LoadingSceneController.Instance.LoadScene("PokerMainScene");
        }

        public void ApplicationQuit()
        {
            Application.Quit();
        }
    }

}
