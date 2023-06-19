using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HwatuDefence
{
    public class PlayerStats : MonoBehaviour
    {
        public static int Money;
        public int startMoney = 0;

        public static int Lives;
        public int startLives = 1;

        public static int Rounds;

        public Text pointText;

        void Start()
        {
            Money = startMoney;
            Lives = startLives;
        }

        void Update()
        {
            pointText.text = "$ " + Money + " / 20".ToString();
        }
    }
}
