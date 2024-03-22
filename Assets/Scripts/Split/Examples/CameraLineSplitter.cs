using MeshSplitting.Splitters;
using System;
using System.Collections;
using UnityEngine;

namespace MeshSplitting.Examples
{
    [AddComponentMenu("Mesh Splitting/Examples/Camera Line Splitter")]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(LineRenderer))]
    public class CameraLineSplitter : MonoBehaviour
    {

        public float CutPlaneDistance = 100f;
        public float CutPlaneSize = 2f;

        private LineRenderer _lineRenderer;
        private Camera _camera;
        private Transform _transform;

        private bool _inCutMode = false;
        private bool _hasStartPos = false;
        private Vector3 _startPos;
        private Vector3 _endPos;

        public GameObject pointUp, pointDown;
        public GameObject player;
        private void Awake()
        {
            _transform = transform;
            _lineRenderer = GetComponent<LineRenderer>();
            _camera = GetComponent<Camera>();

            _lineRenderer.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Start()
        {
            StartCoroutine(Controll());
            Invoke("EnterE", 1);
        }
        private void FixedUpdate()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    _inCutMode = true;
            //    Cursor.lockState = CursorLockMode.None;
            //    Cursor.visible = true;
            //}
            //else if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    _inCutMode = false;
            //    _lineRenderer.enabled = false;
            //    _hasStartPos = false;
            //    Cursor.lockState = CursorLockMode.Locked;
            //    Cursor.visible = false;
            //}

            //if (_inCutMode)
            //{
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        _startPos = GetMousePosInWorld();
            //        _hasStartPos = true;
            //    }
            //    else if (_hasStartPos && Input.GetMouseButtonUp(0))
            //    {
            //        _endPos = GetMousePosInWorld();
            //        if (_startPos != _endPos)
            //            CreateCutPlane();

            //        _hasStartPos = false;
            //        _lineRenderer.enabled = false;
            //    }

            //    if (_hasStartPos)
            //    {
            //        _lineRenderer.enabled = true;
            //        _lineRenderer.SetPosition(0, _startPos);
            //        _lineRenderer.SetPosition(1, GetMousePosInWorld());
            //    }
            //}
        }
        IEnumerator Controll()
        {
            while (true)
            { 
                if (Input.GetKey(KeyCode.E))
                {
                    player.GetComponent<PlayerControll>().RotateRight();
                    EnterE();
                } else if (Input.GetKey(KeyCode.Q))
                {
                    player.GetComponent<PlayerControll>().RotateLeft();
                    EnterE();
                }
                yield return new WaitForSeconds(0.2f);
            }
            
        }

        void EnterE()
        {
            
            Ray ray = new Ray(transform.position, transform.forward * 50 + transform.up * 50);
            _startPos = ray.origin + ray.direction * CutPlaneDistance;
            Debug.DrawRay(transform.position, transform.forward * 50 + transform.up * 50, Color.green);

            Ray ray2 = new Ray(transform.position, transform.forward * 50 + -transform.up * 50);
            _endPos = ray2.origin + ray2.direction * CutPlaneDistance;
            Debug.DrawRay(transform.position, transform.forward * 50 + -transform.up * 50, Color.green);

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _startPos);
            _lineRenderer.SetPosition(1, _endPos);

            CreateCutPlane();
            Camera.main.GetComponent<ControllerMap>().RemoveAllList();

            if (player.GetComponent<Rigidbody>().useGravity == false)
            {
                player.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        //private Vector3 GetMousePosInWorld()
        //{
        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //    return ray.origin + ray.direction * CutPlaneDistance;
        //}

        private void CreateCutPlane()
        {
            Vector3 center = Vector3.Lerp(_startPos, _endPos, .5f);
            Vector3 cut = (_endPos - _startPos).normalized;
            Vector3 fwd = (center - transform.position).normalized;
            Vector3 normal = Vector3.Cross(fwd, cut).normalized;

            GameObject goCutPlane = new GameObject("CutPlane", typeof(BoxCollider), typeof(Rigidbody), typeof(SplitterSingleCut));

            goCutPlane.GetComponent<Collider>().isTrigger = true;
            Rigidbody bodyCutPlane = goCutPlane.GetComponent<Rigidbody>();
            bodyCutPlane.useGravity = false;
            bodyCutPlane.isKinematic = true;

            Transform transformCutPlane = goCutPlane.transform;
            transformCutPlane.position = center;
            transformCutPlane.localScale = new Vector3(CutPlaneSize, .01f, CutPlaneSize);
            transformCutPlane.up = normal;
            float angleFwd = Vector3.Angle(transformCutPlane.forward, fwd);
            transformCutPlane.RotateAround(center, normal, normal.y < 0f ? -angleFwd : angleFwd);
        }
    }
}
