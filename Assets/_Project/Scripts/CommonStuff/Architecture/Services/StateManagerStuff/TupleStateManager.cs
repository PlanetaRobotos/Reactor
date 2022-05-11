using System;

namespace _Project.Scripts.Mechanics
{
    public class TupleStateManager : IStateManager
    {
        public void EnterAction(ref bool isEntered, Action action = null)
        {
            if (isEntered) return;

            isEntered = true;
            action?.Invoke();
        }

        public void ChangeState<T>(ref (T, bool) state, T stateElement)
        {
            state.Item1 = stateElement;
            state.Item2 = false;
        }
    }

    public interface IStateManager
    {
        void EnterAction(ref bool isEntered, Action action = null);
        void ChangeState<T>(ref (T, bool) state, T stateElement);
    }
}