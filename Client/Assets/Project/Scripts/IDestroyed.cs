using System;

namespace Project.Scripts
{
    public interface IDestroyed
    {
        public event Action Destroyed;
    }
}