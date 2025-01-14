using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image joystickBackground;
    [SerializeField] private Image joystickHandle;

    private Vector2 _inputVector; public Vector2 inputVector => _inputVector;

    void Start()
    {
        joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out position);

        position.x = position.x / (joystickBackground.rectTransform.sizeDelta.x / 2);
        position.y = position.y / (joystickBackground.rectTransform.sizeDelta.y / 2);


        _inputVector = new Vector2(position.x, position.y);
        if (_inputVector.magnitude > 1)
        {
            _inputVector = _inputVector.normalized;
        }

        float offSetX = joystickBackground.rectTransform.sizeDelta.x / 2;
        float offSetY = joystickBackground.rectTransform.sizeDelta.y / 2;

        joystickHandle.rectTransform.anchoredPosition = new Vector2(_inputVector.x * offSetX, _inputVector.y * offSetY);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
    }

}
