using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private float _disappearTimer;
    private Color _textColor;
    private Vector3 _moveVector;
    
    private const float DisappearTimerMax = 1f;
    
    private void Awake()
    {
        _textMeshPro = transform.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        MoveObject();
        ScaleObject();
        HandleDisappearTimer();
    }

    private void MoveObject()
    {
        const float moveYSpeed = 5f;
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * (moveYSpeed * Time.deltaTime);
    }
    
    private void ScaleObject()
    {
        const float increaseScaleAmount = 1f;
        const float decreaseScaleAmount = 1f;
        if (_disappearTimer > DisappearTimerMax * 0.5f)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * (increaseScaleAmount * Time.deltaTime);
        }
        else
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * (decreaseScaleAmount * Time.deltaTime);
        }
    }
    
    private void HandleDisappearTimer()
    {
        _disappearTimer -= Time.deltaTime;

        if (_disappearTimer < 0)
        {
            FadeOutAndDestroy();
        }
    }

    private void FadeOutAndDestroy()
    {
        _textColor.a -= 3f * Time.deltaTime;
        _textMeshPro.color = _textColor;

        if (_textColor.a < 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void Setup(int damageAmount, bool isCritical)
    {
        _textMeshPro.SetText(damageAmount.ToString());
        _disappearTimer = DisappearTimerMax;

        if (isCritical)
        {
            _textMeshPro.fontSize = 12;
            _textMeshPro.color = Color.red;
        }
        else
        {
            _textMeshPro.fontSize = 10;
        }

        _moveVector = new Vector3(.7f, 4f) * 3f;
    }
}
