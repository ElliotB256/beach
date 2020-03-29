using UnityEngine;

namespace Beach.Player
{
    public class QuitBehaviour : MonoBehaviour
    {
        protected void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
