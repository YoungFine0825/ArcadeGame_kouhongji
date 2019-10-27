using JW.Common;
using JW.Framework.Asset;
using JW.Framework.Schedule;
using JW.Framework.State;

namespace JW.Framework.MVC
{
    public class ModuleService : Singleton<ModuleService>, IStateCallback, IScheduleHandler
    {
        private JWArrayList<ModuleBase> _modules;
        public override bool Initialize()
        {
            StateService.GetInstance().AddCallback(this);
            this.AddTimer(10000, true);
            _modules = new JWArrayList<ModuleBase>();
            return true;
        }

        public override void Uninitialize()
        {
            for (int i = _modules.Count - 1; i >= 0; --i)
            {
                _modules[i].Destroy();
            }

            _modules.Clear();
            _modules.Release();
            _modules = null;

            StateService.GetInstance().RemoveCallback(this);
            this.RemoveTimer();
        }

        public void OnStateEnter(string state, string fromState, object userData)
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnStateEnter(state, fromState, userData);
            }
        }

        public void OnStateLeave(string state, string toState, object userData)
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnStateLeave(state, toState, userData);
            }
        }

        public void OnStateOverride(string state, string toState, object userData)
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnStateOverride(state, toState, userData);
            }
        }

        public void OnStateResume(string state, string fromState, object userData)
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnStateResume(state, fromState, userData);
            }
        }

        //时钟
        public void OnScheduleHandle(ScheduleType type, uint id)
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                if (_modules[i] != null)
                {
                    _modules[i].ProcessControl();
                }
            }
        }

        public ModuleBase Create<T>() where T : ModuleBase, new()
        {
            T module = new T();
            module.Create();

            _modules.Add(module);
            return module;
        }

        // ReSharper disable once UnusedMember.Local
        public void Create<T, TM>() where T : ModuleBase, new()
                                     where TM : UIMediator, new()
        {
            ModuleBase module = Create<T>();
            module.CreateMediator<TM>();
        }

        // ReSharper disable once UnusedMember.Local
        public void Create<T, TM1, TM2>() where T : ModuleBase, new()
                                           where TM1 : UIMediator, new()
                                           where TM2 : UIMediator, new()
        {
            ModuleBase module = Create<T>();
            module.CreateMediator<TM1>();
            module.CreateMediator<TM2>();
        }

        // ReSharper disable once UnusedMember.Local
        public void Create<T, TM1, TM2, TM3>() where T : ModuleBase, new()
                                           where TM1 : UIMediator, new()
                                           where TM2 : UIMediator, new()
                                           where TM3 : UIMediator, new()
        {
            ModuleBase module = Create<T>();
            module.CreateMediator<TM1>();
            module.CreateMediator<TM2>();
            module.CreateMediator<TM3>();
        }

        // ReSharper disable once UnusedMember.Local
        public void Create<T, TM1, TM2, TM3, TM4>() where T : ModuleBase, new()
                                           where TM1 : UIMediator, new()
                                           where TM2 : UIMediator, new()
                                           where TM3 : UIMediator, new()
                                           where TM4 : UIMediator, new()
        {
            ModuleBase module = Create<T>();
            module.CreateMediator<TM1>();
            module.CreateMediator<TM2>();
            module.CreateMediator<TM3>();
            module.CreateMediator<TM4>();
        }

        // ReSharper disable once UnusedMember.Local
        public void Create<T, TM1, TM2, TM3, TM4, TM5>() where T : ModuleBase, new()
                                           where TM1 : UIMediator, new()
                                           where TM2 : UIMediator, new()
                                           where TM3 : UIMediator, new()
                                           where TM4 : UIMediator, new()
                                           where TM5 : UIMediator, new()
        {
            ModuleBase module = Create<T>();
            module.CreateMediator<TM1>();
            module.CreateMediator<TM2>();
            module.CreateMediator<TM3>();
            module.CreateMediator<TM4>();
            module.CreateMediator<TM5>();
        }

    }
}
