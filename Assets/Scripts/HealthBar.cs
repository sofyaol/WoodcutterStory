using DefaultNamespace;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
   [SerializeField] private GameObject _components;
   [SerializeField] private float _timeOfVisability = 3f;
    private Slider _slider;
    private ICharacter _character;

    private bool _isVisible;
    private float _timer;
    void Start()
    {
        _slider = GetComponent<Slider>();
        _character = GetComponentInParent<ICharacter>();
        _character.OnHealthChange += OnHealthChange;
    }

    private void Update()
    {
        if (_isVisible)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeOfVisability)
            {
                MakeVisible(false);
            }
        }
    }

    void OnHealthChange(float maxHealth, float health)
    {
        MakeVisible(true);
        
        if (health <= 0)
        {
            _character.OnHealthChange -= OnHealthChange;
            gameObject.SetActive(false);

            return;
        }

        _slider.value = health / maxHealth;
    }

    
    // Get damage -> MakeVisible() -> делаем визибл, timer = 0 (если таймер > T, то выключаем висибл)
    internal void MakeVisible(bool value)
    {
        _isVisible = value;
        _components.SetActive(_isVisible);
        if (!_isVisible) return;

        _timer = 0;
    }
}
