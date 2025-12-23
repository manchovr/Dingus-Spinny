using BepInEx;
using BepInEx.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace DingusSpinny
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private AssetBundle bundle;
        private GameObject asset;
        public static GameObject spawnedDingus;
        public static GameObject dingus;
        public static GameObject kaboom;
        public static ConfigEntry<float> configSpinSpeed;
        public static float timer = 0f;

        void Update()
        {
            if (spawnedDingus != null)
            {
                spawnedDingus.transform.Rotate( 0f, configSpinSpeed.Value * Time.deltaTime, 0f, Space.World );
            }
            if (UnityInput.Current.GetKeyDown(KeyCode.Backslash))
            {
                dingus.SetActive(true);
                kaboom.SetActive(false);
                DingusTouchHandler.dingusblewuplol = false;
            }
            if (DingusTouchHandler.dingusblewuplol)
            {
                timer += Time.deltaTime;

                if (timer >= 10f)
                {
                    DingusTouchHandler.dingusblewuplol = false;
                    dingus.SetActive(true);
                }
            }
        }

        void OnPlayerSpawned()
        {
            SpawnDingus();
        }
        void SpawnDingus()
        {
            spawnedDingus = Instantiate(asset, new Vector3(-65.7588f, 11.6926f, -80.1358f), Quaternion.Euler(0f, 234f, 0f));
            dingus = spawnedDingus.transform.GetChild(0).gameObject;
            kaboom = spawnedDingus.transform.GetChild(1).gameObject;

            kaboom.SetActive(false);
            SetupInteraction(dingus);
        }

        void Start()
        {
            bundle = LoadAssetBundle("DingusSpinny.assets.dingus");
            asset = bundle.LoadAsset<GameObject>("dingus root");

            GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);

            configSpinSpeed = Config.Bind("General", "Spin Speed", 90f, "dingus spinny speed yeah!");
        }

        void SetupInteraction(GameObject dingus)
        {
            MeshCollider meshCollider = dingus.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.convex = true;
                meshCollider.isTrigger = true;
            }

            Rigidbody rigidbody = dingus.GetComponent<Rigidbody>();
            if (rigidbody == null)
                rigidbody = dingus.AddComponent<Rigidbody>();

            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            if (dingus.GetComponent<DingusTouchHandler>() == null)
                dingus.AddComponent<DingusTouchHandler>();
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
    }
}
