using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minZoomValue;
    [SerializeField] private float _maxZoomValue;

    private float _currentZoomValue = 5f;


    public void OnMouseScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<Vector2>().y;
        _currentZoomValue -= scrollValue * _zoomSpeed * Time.deltaTime;

        _currentZoomValue = Mathf.Clamp(_currentZoomValue, _minZoomValue, _maxZoomValue);

        transform.localScale = new Vector3(_currentZoomValue, _currentZoomValue, _currentZoomValue);
    }
}
