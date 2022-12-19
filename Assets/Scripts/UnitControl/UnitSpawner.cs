using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
	GWANG_TTAENG38,       // 광땡
	GWANG_TTAENG18,       // 광땡
	GWANG_TTAENG13,       // 광땡
	TTAENG,               // 땡
	AL_LI,                // 알리
	DOK_SA,               // 독사
	KKERUS9,         	  // 구삥
	KKERUS10,        	  // 장삥
	JANG_SA,          	  // 장사
	SEL_YUG,          	  // 세륙
	GAB_O,            	  // 갑오
	KKEUS,            	  // 끗
	SECRET_ROYAL_PALACE,  // 암행어사
	MANG_TONG,            // 망통
	TTAENG_COUNTER,   	  // 떙잡이
	None
}

public class UnitSpawner : MonoBehaviour
{
	[SerializeField]
	private	GameObject	unitPrefab;
	[SerializeField]
	private	int			maxUnitCount;

	[Header("유닛 기본 모델")]
	[SerializeField]
	private GameObject unitModel;

	public	Vector2		minSize = new Vector2(-3, -3);
	public	Vector2		maxSize = new Vector2(3, 3);

	public HwatuDefence.UnitSO unitSO;

	[Header("유닛 기본 공격력")]
	[SerializeField]
	private int unit_Default_Dmg = 10;

	public List<UnitController> SpawnUnits()
	{
		List<UnitController> unitList = new List<UnitController>(maxUnitCount);

		for ( int i = 0; i < maxUnitCount; ++ i )
		{
			Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 1, Random.Range(minSize.y, maxSize.y));

			GameObject		clone	= Instantiate(unitPrefab, position, Quaternion.identity);
			UnitController	unit	= clone.GetComponent<UnitController>();

			unitList.Add(unit);
		}

		return unitList;
	}

	public List<UnitController> SpawnUnits(UnitType type, int cardNum)
	{
		List<UnitController> unitList = new List<UnitController>(1);

		Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 1, Random.Range(minSize.y, maxSize.y));

		GameObject		clone	= Instantiate(unitModel, position, Quaternion.identity);
		
		for(int i = 0; i < unitSO.unitScriptableList.Count; i++)
		{
			if((int)type == i)
			{
				clone.GetComponent<HwatuDefence.UnitProcess>().SetUnitSO(unitSO.unitScriptableList[i]);
			}
		}

		clone.GetComponent<HwatuDefence.UnitProcess>().SetUnitAttackValues(unit_Default_Dmg, cardNum);

		UnitController	unit	= clone.GetComponent<UnitController>();

		unitList.Add(unit);

		return unitList;
	}

	// 테스트
	// private void Start() {
	// 	SpawnUnits(UnitType.GWANG_TTAENG38);
	// }
}

