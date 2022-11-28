using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		unitMarker;
	private	NavMeshAgent	navMeshAgent;
	
	[SerializeField]
	private	LineRenderer	unitRadiusMarker;

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	public void SelectUnit()
	{
		unitMarker.SetActive(true);
	}

	public void DeselectUnit()
	{
		unitMarker.SetActive(false);
	}

	public void MoveTo(Vector3 end)
	{
		navMeshAgent.SetDestination(end);
	}

	public void ShowRadius()
	{
		unitRadiusMarker.enabled = true;
	}

	public void UnShowRadius()
	{
		unitRadiusMarker.enabled = false;
	}

	public void Stop()
	{
		navMeshAgent.ResetPath(); 				// SetDestination(pos) 리셋
		navMeshAgent.enabled.Equals(false); 	// NavMeshAgent 컴포넌트를 비활성화 하여 바로 멈추게 함
		navMeshAgent.enabled.Equals(true); 		// 비활성화 후, 바로 활성화 하여 다음 동작 대기
	}

}

