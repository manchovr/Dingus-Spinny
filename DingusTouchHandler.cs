using UnityEngine;
using System.Collections;

namespace DingusSpinny
{
    public class DingusTouchHandler : MonoBehaviour
    {
        public static bool dingusblewuplol = false;

        private void OnTriggerEnter(Collider other)
        {
            DingusStuff();
        }

        void DingusStuff()
        {
            Debug.Log("kaboom");
            Plugin.dingus.SetActive(false);
            Plugin.kaboom.SetActive(true);
            dingusblewuplol = true;
            Plugin.timer = 0f;
        }
    }
}
