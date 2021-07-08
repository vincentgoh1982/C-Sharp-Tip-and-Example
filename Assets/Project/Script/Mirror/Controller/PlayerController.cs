using Mirror;
using UnityEngine;

namespace QuickStart
{
    public class PlayerController : NetworkBehaviour
    {
        public TextMesh playerNameText;
        public GameObject floatingInfo;
        private SceneScript sceneScript;

        private Weapon activeWeapon;
        private float weaponCooldownTime;

        private Material playerMaterialClone;

        private int selectedWeaponLocal = 1;
        public GameObject[] weaponArray;

        [SyncVar(hook = nameof(OnWeaponChanged))]
        public int activeWeaponSynced = 1;

        [SyncVar(hook = nameof(OnNameChanged))]//fires for changed values
        public string playerName;

        [SyncVar(hook = nameof(OnColorChanged))]//fires for changed values
        public Color playerColor = Color.white;

        void OnWeaponChanged(int _Old, int _New)
        {
            // disable old weapon
            // in range and not null
            if (0 < _Old && _Old < weaponArray.Length && weaponArray[_Old] != null)
            {
                weaponArray[_Old].SetActive(false);
            }

            // enable new weapon
            // in range and not null
            if (0 < _New && _New < weaponArray.Length && weaponArray[_New] != null)
            {
                weaponArray[_New].SetActive(true);
                activeWeapon = weaponArray[activeWeaponSynced].GetComponent<Weapon>();
                if (isLocalPlayer) { sceneScript.UIAmmo(activeWeapon.weaponAmmo); }
            }
        }

        [Command]
        public void CmdChangeActiveWeapon(int newIndex)
        {
            activeWeaponSynced = newIndex;
        }

        void Awake()
        {
            // disable all weapons
            foreach (var item in weaponArray)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }


            //allow all players to run this
            sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;

            if (selectedWeaponLocal < weaponArray.Length && weaponArray[selectedWeaponLocal] != null)
            {
                activeWeapon = weaponArray[selectedWeaponLocal].GetComponent<Weapon>(); sceneScript.UIAmmo(activeWeapon.weaponAmmo);
            }
        }

        [Command]//Commands tell the server what a client wants to change.
        public void CmdSendPlayerMessage()
        {
            if (sceneScript)
            {
                sceneScript.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
            }
        }
        [Command]//Commands tell the server what a client wants to change.
        public void CmdSetupPlayer(string _name, Color _col)
        {
            //player info sent to server, then server updates sync vars which handles it on all clients
            playerName = _name;
            playerColor = _col;
            sceneScript.statusText = $"{playerName} joined.";
        }

        void OnNameChanged(string _Old, string _New)
        {
            playerNameText.text = playerName;
        }

        void OnColorChanged(Color _Old, Color _New)
        {
            playerNameText.color = _New;
            playerMaterialClone = new Material(GetComponent<Renderer>().material);
            playerMaterialClone.color = _New;
            this.GetComponent<Renderer>().material = playerMaterialClone;
        }

        public override void OnStartLocalPlayer()//Like Start(), but only called on client and host for the local player object.
        {
            sceneScript.cameraObject.transform.SetParent(transform);
            sceneScript.cameraObject.transform.localPosition = new Vector3(0, 0, 0);

            floatingInfo.transform.localPosition = new Vector3(0, -0.3f, 0.6f);
            floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            string name = "Player" + Random.Range(100, 999);
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            CmdSetupPlayer(name, color);
        }

        void Update()
        {
            if (!isLocalPlayer)
            {
                // make non-local players run this
                floatingInfo.transform.LookAt(Camera.main.transform);
                return;
            }

            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            transform.Rotate(0, moveX, 0);
            transform.Translate(0, 0, moveZ);

            if (Input.GetButtonDown("Fire2")) //Fire2 is mouse 2nd click and left alt
            {
                selectedWeaponLocal += 1;

                if (selectedWeaponLocal > weaponArray.Length)
                {
                    selectedWeaponLocal = 1;
                }

                CmdChangeActiveWeapon(selectedWeaponLocal);
            }

            if (Input.GetButtonDown("Fire1")) //Fire1 is mouse 1st click
            {
                if (activeWeapon && Time.time > weaponCooldownTime && activeWeapon.weaponAmmo > 0)
                {
                    weaponCooldownTime = Time.time + activeWeapon.weaponCooldown;
                    activeWeapon.weaponAmmo -= 1;
                    sceneScript.UIAmmo(activeWeapon.weaponAmmo);
                    CmdShootRay();
                }
            }
        }

        [Command]
        void CmdShootRay()
        {
            RpcFireWeapon();
        }

        [ClientRpc]
        void RpcFireWeapon()
        {
            //bulletAudio.Play(); muzzleflash  etc
            var bullet = (GameObject)Instantiate(activeWeapon.weaponBullet, activeWeapon.weaponFirePosition.position, activeWeapon.weaponFirePosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * activeWeapon.weaponSpeed;
            if (bullet) { Destroy(bullet, activeWeapon.weaponLife); }
        }
    }
}
