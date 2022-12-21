using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HwatuDefence
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject ui;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            ui.SetActive(!ui.activeSelf);

            if(ui.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = GameManager.Inst.GameSpeed;
            }
        }

        public void Retry()
        {
            Toggle();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Menu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
