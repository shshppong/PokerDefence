using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestScript
{
    [System.Serializable]
    public class ArtifactInfo
    {
        [HideInInspector]
        public string elementName;
        public int ID;
        public string itemName;
        public Sprite thumbnailSprites;
        public string description;
        public Sprite[] sprites;
    }

    public enum ArtifactType
    {
        Hair,
        BackHair,
        Head,
        Face,
        Emotion,
        Hat,
        Accessory,
        Robe,
        UpperBody,
        LowerBody,
        BackStuff
    }

    [CreateAssetMenu(fileName = "CardTest", menuName = "CardTest/CardTest", order = int.MaxValue)]

    public class CardDataTest : ScriptableObject
    {
        public ArtifactType typeIndex;
        [SerializeField]
        public List<ArtifactInfo> artifactInfos;

        public void OnValidate()
        {
            for(int i = 0; i < artifactInfos.Count; i++)
            {
                artifactInfos[i].elementName = "[" + ((int)typeIndex * 10000 + i) + "] \t" + artifactInfos[i].itemName;
                artifactInfos[i].ID = ((int)typeIndex * 10000 + i);

                if(artifactInfos[i].thumbnailSprites == null && artifactInfos[i].sprites.Length > 0)
                    artifactInfos[i].thumbnailSprites = artifactInfos[i].sprites[0];
            }
        }
    }
    
}
