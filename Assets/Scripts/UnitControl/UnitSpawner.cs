using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	[SerializeField]
	private	GameObject	unitPrefab;
	[SerializeField]
	private	int			maxUnitCount;

	public	Vector2		minSize = new Vector2(-3, -3);
	public	Vector2		maxSize = new Vector2(3, 3);

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

	public List<UnitController> SpawnUnits(int amount)
	{
		List<UnitController> unitList = new List<UnitController>(amount);

		for ( int i = 0; i < amount; ++ i )
		{
			Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 1, Random.Range(minSize.y, maxSize.y));

			GameObject		clone	= Instantiate(unitPrefab, position, Quaternion.identity);
			UnitController	unit	= clone.GetComponent<UnitController>();

			unitList.Add(unit);
		}

		return unitList;
	}
}

