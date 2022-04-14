using System;

namespace DefaultNamespace
{
    public interface IChoppable 
    {
        public event ChoppableDie Dying;
        public void GetDamage();
        public int Id { get; set; }
    }
    
    public delegate void ChoppableDie(int id);
}