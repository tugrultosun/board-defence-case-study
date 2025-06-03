using System;
using System.Collections.Generic;

namespace States
{
    public class StateManager
    {
        private readonly List<IState> _states;
        private IState _currentState;
        
        public StateManager()
        {
            _states = new List<IState>();
        }
        
        public void AddState(IState state)
        {
            _states.Add(state);
        }
    
        public void ChangeState(Type state)
        {
            _currentState = _states.Find(s => s.GetType() == state);
        }
    
        public IState GetCurrentState()
        {
            return _currentState;
        }
        
        public Type GetCurrentStateType()
        {
            return _currentState?.GetType();
        }
    }
}
