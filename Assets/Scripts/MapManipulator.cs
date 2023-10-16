namespace Mapbox.Examples
{
    using Mapbox.Unity.Map;
    using Mapbox.Unity.Utilities;
    using Mapbox.Utils;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using System;

    public class MapManipulator : MonoBehaviour
    {
        [SerializeField]
        public float _panSpeed = 1.0f;

        [SerializeField]
        public float _zoomSpeed = 0.25f;

        [SerializeField]
        AbstractMap _mapManager;

        [SerializeField]
        bool _useDegreeMethod;

        private Vector3 _origin;
        private Vector3 _mousePosition;
        private Vector3 _mousePositionPrevious;
        private bool _shouldDrag;
        private bool _isInitialized = false;
        private Plane _groundPlane = new Plane(Vector3.up, 0);
        private bool _isDragging = false;

        void Awake()
        {
            _mapManager.OnInitialized += () =>
            {
                _isInitialized = true;
            };
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()) // If user touches map with index finger
            {
                _isDragging = true;
            }

            if (Input.GetMouseButtonUp(0)) // Touch ended
            {
                _isDragging = false;
            }
        }


        private void LateUpdate()
        {
            if (!_isInitialized) return;

            if (!_isDragging)
            {
                PanMap();
                ZoomMap();
            }
        }

        void ZoomMap()
        {
            float scrollDelta = 0.0f;
            scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            var zoom = Mathf.Max(0.0f, Mathf.Min(_mapManager.Zoom + scrollDelta * _zoomSpeed, 21.0f));
            if (Math.Abs(zoom - _mapManager.Zoom) > 0.0f)
            {
                _mapManager.UpdateMap(_mapManager.CenterLatitudeLongitude, zoom);
            }
        }

        void PanMap()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                _mousePosition = Input.mousePosition;

                if (_shouldDrag == false)
                {
                    _shouldDrag = true;
                    _origin = _mousePosition;
                }
            }
            else
            {
                _shouldDrag = false;
            }

            if (_shouldDrag == true)
            {
                var changeFromPreviousPosition = _mousePositionPrevious - _mousePosition;
                if (Mathf.Abs(changeFromPreviousPosition.x) > 0.0f || Mathf.Abs(changeFromPreviousPosition.y) > 0.0f)
                {
                    _mousePositionPrevious = _mousePosition;
                    var offset = _origin - _mousePosition;

                    if (Mathf.Abs(offset.x) > 0.0f || Mathf.Abs(offset.z) > 0.0f)
                    {
                        if (null != _mapManager)
                        {
                            float factor = _panSpeed * Conversions.GetTileScaleInMeters(0.0f, _mapManager.AbsoluteZoom) / _mapManager.UnityTileSize;
                            var latlongDelta = Conversions.MetersToLatLon(new Vector2d(offset.x * factor, offset.z * factor));
                            var newLatLong = _mapManager.CenterLatitudeLongitude + latlongDelta;

                            _mapManager.UpdateMap(newLatLong, _mapManager.Zoom);
                        }
                    }
                    _origin = _mousePosition;
                }
                else
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }
                    _mousePositionPrevious = _mousePosition;
                    _origin = _mousePosition;
                }
            }
        }
    }
}