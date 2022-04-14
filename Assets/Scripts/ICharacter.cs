using System;
namespace DefaultNamespace
{
    public interface ICharacter
    {
        public event HealthChange  OnHealthChange;
        public float Health { get; set; }
    }

    public delegate void HealthChange(float maxHealth, float health);
}