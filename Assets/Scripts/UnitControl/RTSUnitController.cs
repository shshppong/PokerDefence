using System;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnitController : MonoBehaviour
{
	[SerializeField]
	private	UnitSpawner			 unitSpawner;
	private	List<UnitController> selectedUnitList;				// 플레이어가 클릭 or 드래그로 선택한 유닛
	public	List<UnitController> UnitList { private set; get; }	// 맵에 존재하는 모든 유닛

	private void Awake()
	{
		selectedUnitList = new List<UnitController>();
		// UnitList		 = unitSpawner.SpawnUnits();
		UnitList = new List<UnitController>();
	}

	/// <summary>
	/// 다른 스크립트에서 유닛 적재 허용
	/// </summary>
    internal void AddRangeUnitList(List<UnitController> unitControllers)
    {
		UnitList.AddRange(unitControllers);
    }

	/// <summary>
	/// 마우스 클릭으로 유닛을 선택할 때 호출
	/// </summary>
	public void ClickSelectUnit(UnitController newUnit)
	{
		// 기존에 선택되어 있는 모든 유닛 해제
		DeselectAll();

		SelectUnit(newUnit);
	}

	/// <summary>
	/// Shift+마우스 클릭으로 유닛을 선택할 때 호출
	/// </summary>
	public void ShiftClickSelectUnit(UnitController newUnit)
	{
		// 기존에 선택되어 있는 유닛을 선택했으면
		if ( selectedUnitList.Contains(newUnit) )
		{
			DeselectUnit(newUnit);
		}
		// 새로운 유닛을 선택했으면
		else
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// 마우스 드래그로 유닛을 선택할 때 호출
	/// </summary>
	public void DragSelectUnit(UnitController newUnit)
	{
		// 새로운 유닛을 선택했으면
		if ( !selectedUnitList.Contains(newUnit) )
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// 선택된 모든 유닛을 이동할 때 호출
	/// </summary>
	public void MoveSelectedUnits(Vector3 end)
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].MoveTo(end);
		}
	}

	/// <summary>
	/// 모든 유닛의 선택을 해제할 때 호출
	/// </summary>
	public void DeselectAll()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].DeselectUnit();
		}

		selectedUnitList.Clear();
	}

	/// <summary>
	/// 매개변수로 받아온 newUnit 선택 설정
	/// </summary>
	private void SelectUnit(UnitController newUnit)
	{
		// 유닛이 선택되었을 때 호출하는 메소드
		newUnit.SelectUnit();
		// 선택한 유닛 정보를 리스트에 저장
		selectedUnitList.Add(newUnit);
	}

	/// <summary>
	/// 매개변수로 받아온 newUnit 선택 해제 설정
	/// </summary>
	private void DeselectUnit(UnitController newUnit)
	{
		// 유닛이 해제되었을 때 호출하는 메소드
		newUnit.DeselectUnit();
		// 선택한 유닛 정보를 리스트에서 삭제
		selectedUnitList.Remove(newUnit);
	}

	// 개인 추가사항

	/// <summary>
	/// 선택된 모든 유닛을 키보드 입력으로 멈출 때 호출
	/// </summary>
	public void MoveToStop()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].Stop();
		}
	}
	
	/// <summary>
	/// 필드에 있는 선택된 유닛 사거리 표시
	/// </summary>
	public void AllUnitShowRadius()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].ShowRadius();
		}
	}

	/// <summary>
	/// 필드에 있는 선택된 유닛 사거리 비 표시
	/// </summary>
	public void AllUnitUnShowRadius()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].UnShowRadius();
		}
	}

	/// <summary>
	/// 선택된 유닛을 리턴 (최상위 단일)
	/// </summary>
	public GameObject ReturnGameObject()
	{
		if (selectedUnitList.Count > 0)
		{
			return selectedUnitList[0].gameObject;
		}
		return null;
	}
}

