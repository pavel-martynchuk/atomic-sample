using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

public class OrbitAroundParent : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private IAtomicVariable<Transform> _targetObject;

    [SerializeField] private float _rotationSpeed = 30f; // Скорость поворота
    [SerializeField] private float _radius = 0.5f;       // Радиус орбиты

    private Vector3 _direction;
    private float _initialYPosition;  // Сохраняем изначальное значение Y

    public void Compose(IAtomicVariable<Transform> target)
    {
        _targetObject = target;
        _initialYPosition = transform.position.y;  // Инициализация начального значения Y
    }

    private void Update()
    {
        if (_targetObject.Value != null)
        {
            // Если есть цель, устанавливаем направление на нее
            SetDirectionToTarget();
        }
        else
        {
            // Если цели нет, сбрасываем направление
            SetForwardDirection();
        }

        // Перемещаем объект на заданный радиус по текущему направлению
        MoveOnOrbit();
    }

    private void SetDirectionToTarget()
    {
        Vector3 targetPosition = _targetObject.Value.position;
        Vector3 objectPosition = transform.position;

        // Рассчитываем направление на цель
        _direction = targetPosition - objectPosition;

        // Игнорируем ось Y для поворота только по горизонтали
        _direction.y = 0;

        if (_direction != Vector3.zero)
        {
            // Рассчитываем нужный поворот по направлению к цели
            Quaternion targetRotation = Quaternion.LookRotation(_direction);

            // Плавно поворачиваем объект к цели с учетом скорости вращения
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void SetForwardDirection()
    {
        // Устанавливаем направление "вперед" (вдоль оси Z)
        _direction = transform.forward;

        // Игнорируем ось Y для поворота только по горизонтали
        _direction.y = 0;

        // Сбрасываем поворот к начальному состоянию (вдоль оси Z)
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, defaultRotation, _rotationSpeed * Time.deltaTime);
    }

    // Логика перемещения объекта на заданный радиус по текущему направлению
    private void MoveOnOrbit()
    {
        if (_direction != Vector3.zero)
        {
            // Нормализуем направление, чтобы оно было единичным
            Vector3 normalizedDirection = _direction.normalized;

            // Вычисляем новую позицию объекта на радиусе от текущего положения
            Vector3 newPosition = transform.parent.position + normalizedDirection * _radius;

            // Обновляем позицию объекта, фиксируя значение по оси Y
            newPosition.y = _initialYPosition;
            transform.position = newPosition;
        }
    }
}
