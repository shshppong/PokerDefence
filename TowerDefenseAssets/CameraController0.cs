using UnityEngine;

namespace TowerDefenseAssets
{
    public class CameraController : MonoBehaviour
    {
        private bool doMovement = true;

        public float panSpeed = 30f;
        public float panBorderThickness = 10f;

        public float scrollSpeed = 5f;
        public float minY = 10f;
        public float maxY = 80f;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                doMovement = !doMovement;
            }

            if(!doMovement) return;

            if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                transform.position += Vector3.forward * panSpeed * Time.deltaTime;
            }
            if(Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                transform.position += Vector3.back * panSpeed * Time.deltaTime;
            }
            if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                transform.position += Vector3.right * panSpeed * Time.deltaTime;
            }
            if(Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                transform.position += Vector3.left * panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            
            Vector3 pos = transform.position;

            pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;

        }
    }

}
