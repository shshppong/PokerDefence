using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace HwatuDefence
{
    public class GameManager : MonoBehaviour
    {
        [Header("게임 배속 조절")]
        [SerializeField]
        [Range(1, 4)]
        private int gameSpeed = 1;            // Default : 1x (1배속)
        public int GameSpeed { get { return gameSpeed; } }

        public GameObject gameOverObj;

        public void GameOver()
        {
            gameSpeed = 0;
            Debug.Log("게임 종료!");
            gameOverObj.SetActive(true);
        }

        public static GameManager Inst { get; private set; }
        void Awake()
        {
            Inst = this;
            Application.targetFrameRate = 60;
            gameOverObj.SetActive(false);
        }

        void Start()
        {
            StartCoroutine(DownloadSO());
        }

        void FixedUpdate()
        {
            Time.timeScale = gameSpeed;
        }

        #region 스크립터블 오브젝트

        [SerializeField] UnitSO unitSO;
        const string URL = "https://docs.google.com/spreadsheets/d/18gh1Ez3aEH3AydC1FgUpSrUVRoQDa-dP1NSMJThYpoI/export?format=tsv&range=A2:E17";

        IEnumerator DownloadSO()
        {
            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            string data = www.downloadHandler.text;
            SetUnitSO(data);
        }

        void SetUnitSO(string tsv)
        {
            string[] row = tsv.Split('\n');
            int rowSize = row.Length;
            int columnSize = row[0].Split('\t').Length;

            for(int i = 0; i < rowSize; i++)
            {
                string[] column = row[i].Split('\t');
                for(int j = 0; j < columnSize; j++)
                {
                    UnitData target = unitSO.unitScriptableList[i];
                    target.name = column[0];
                    target.unitName = column[0];
                    target.attack = int.Parse(column[1]);
                    target.attackSpeed = float.Parse(column[2]);
                    target.attackRange = float.Parse(column[3]);
                    target.moveSpeed = float.Parse(column[4]);
                }
            }
        }
        #endregion
    }
}
