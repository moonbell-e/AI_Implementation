using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private float _zoomDampening = 7.5f;
    [SerializeField] private float _stepSize = 2f;
    [SerializeField] private float _minHeight = 5f;
    [SerializeField] private float _maxHeight = 20f;

    private float _zoomHeight;
    private Transform _cameraTransform;
    private Inputs _inputs;

    private void Awake()
    {
        _inputs = new Inputs();
        _cameraTransform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _zoomHeight = _cameraTransform.localPosition.y;
        _cameraTransform.LookAt(this.transform);
        _inputs.MouseScroll.Zoom.performed += OnMouseScroll;
        _inputs.MouseScroll.Enable();
    }

    private void OnDisable()
    {
        _inputs.MouseScroll.Zoom.performed -= OnMouseScroll;
        _inputs.MouseScroll.Disable();
    }

    private void Update()
    {
        UpdateCameraPosition();
    }

    public void OnMouseScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(scrollValue) > 0.1f)
        {
            scrollValue *= -1f;

            _zoomHeight = _cameraTransform.localPosition.y + scrollValue * _stepSize;

            if (_zoomHeight < _minHeight)
                _zoomHeight = _minHeight;
            else if (_zoomHeight > _maxHeight)
                _zoomHeight = _maxHeight;
        }
    }

    private void UpdateCameraPosition()
    {

        Vector3 zoomTarget = new Vector3(_cameraTransform.localPosition.x, _zoomHeight, _cameraTransform.localPosition.z);

        zoomTarget -= _zoomSpeed * (_zoomHeight - _cameraTransform.localPosition.y) * _cameraTransform.forward;

        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, zoomTarget, Time.deltaTime * _zoomDampening);
    }
}
