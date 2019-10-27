using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JW.Framework.ArcadeInput
{
    public class Demo1 : MonoBehaviour
    {

        public Transform UpKey;
        public Transform DownKey;
        public Transform LeftKey;
        public Transform RightKey;

        public Transform LeftUpKey;
        public Transform LeftDownKey;
        public Transform RightUpKey;
        public Transform RightDownKey;
        public Transform SureKey;
        public Transform RotateCube;

        public Text KeyFlag;

        public Button button0;
        public Button button1;

        byte mode0 = 0;
        byte mode1 = 0;


        private void Awake()
        {

        }

        //protected void AddNotifyListener()
        //{
        //    NotificationCenter.GetInstance().AddHandler(HandleLightBarOnLine, NotificationConst.light_bar_online);
        //}

        //protected void HandleLightBarOnLine(object name, object sender, object[] args)
        //{
        //    Debug.Log("HandleLightBarOnLine");
        //}
        public void LrightBar0CtrlButton()
        {
            mode0++;
            if (mode0 == 16) mode0 = 0;
            McuInput.GetInstance().SetLightBar(0, mode0);
            Text text = button0.GetComponentInChildren(typeof(Text)) as Text;

            text.text = "模式" + mode0.ToString("D");
        }

        public void LrightBar1CtrlButton()
        {
            mode1++;
            if (mode1 == 16) mode1 = 0;
            McuInput.GetInstance().SetLightBar(1, mode1);
            Text text = button1.GetComponentInChildren(typeof(Text)) as Text;

            text.text = "模式" + mode1.ToString("D");
        }


        public void ConnectStateCallback(MCUConnectState state)
        {


            Debug.Log("state = " + state);
            if (state == MCUConnectState.connect)
            {
                KeyFlag.color = new Color(0.0f, 1.0f, 0.0f);

                button0.enabled = true;
                button1.enabled = true;
            }
            else
            {
                KeyFlag.color = new Color(0.0f, 0.0f, 0.0f);

                button0.enabled = false;
                button1.enabled = false;
            }
        }

        // Use this for initialization
        void Start()
        {
           // McuInput.GetInstance().RegisterConnectStateCallBack(ConnectStateCallback);
        }

        // Update is called once per frame
        void Update()
        {

           // McuInput.GetInstance().ConnectStateCallbackLoop();

            if (McuInput.GetInstance().GetKey(MCUKeyCode.Up))
            {
                UpKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.Down))
            {
                DownKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.Left))
            {
                LeftKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.Right))
            {
                RightKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.Sure))
            {
                SureKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.LeftUp))
            {
                LeftUpKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.LeftDown))
            {
                LeftDownKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.RightUp))
            {
                RightUpKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            if (McuInput.GetInstance().GetKey(MCUKeyCode.RightDown))
            {
                RightDownKey.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }

            int tmp = McuInput.GetInstance().GetRotation();
            if (tmp != 0)
            {
                RotateCube.transform.RotateAround(Vector3.up, (3.1415926f / 180.0f) * tmp);
            }
        }
    }
}