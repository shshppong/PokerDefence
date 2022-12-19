using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HwatuDefence
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemySO : ScriptableObject
    {
        public List<UnitData> EnemyScriptableList;
    }
    
}

/*
1웨이브당 50마리 나오고
50마리 잡으면 50포인트가 생기고
한 번 뽑는데 20포인트가 소모되니
50포인트는 2번 뽑을 수 있다.
1웨이브당 2번 뽑을 수 있다.

만약 최악의 상황일 경우에 보스를 잡아야한다면,
무조건 강화 한 번을 해야 깰 수 있도록 밸런스 조절

강화 비용 15

보스 라운드는 10라운드마다 1번 씩 시작
10라운드는 5번 뽑은 상태가 되고
그러면 5개 다 망통 또는 1끗일 경우 초당 공격력 5

보스 스테이지는 2분을 주고




망통 또는 1끗을 들고

10 스테이지 보스는 체력 65

*/