using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
	[SerializeField]
	private	RTSUnitController rtsUnitController;

    void Update()
    {
        PressKeys();
        ShowUnitRadius();
    }

    // 인스턴스 오류나는건 현재 UnitController는 유닛에서만 사용되고 있고,
    // RTSUnitController를 통해서 UnitController에 전달되기 때문에 유닛들에 접근하려면 
    // UnitController에서 유닛 개별적으로 기능을 추가한 후 연결 당할 RTSUnitController에 기능을 연결해야함.
    // 한줄 요약 : KeyboardInput >>> RTSUnitController >>> UnitController

    private void PressKeys()
    {
        // 정지
        if(Input.GetKeyDown(KeyCode.S))
        {
            rtsUnitController.MoveToStop();
        }

        // 홀딩
        if(Input.GetKeyDown(KeyCode.H))
        {
        }
    }

    // C버튼을 누르고 있는 상태일 때 전체 유닛의 사정 거리를 보여줌.
    private void ShowUnitRadius()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            rtsUnitController.AllUnitShowRadius();
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            rtsUnitController.AllUnitUnShowRadius();
        }
    }
}
