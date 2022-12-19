using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    [CreateAssetMenu(fileName = "UnitSO", menuName = "Unit/UnitSO")]
    public class UnitSO : ScriptableObject
    {
        public List<UnitData> unitScriptableList;
    }
    
}