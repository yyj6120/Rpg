
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    public enum eSingleton
    {
        CreateInstance,
        DestroyInstance,
    }
    public interface ISingleton
    {
        void CallMethod(eSingleton method);
    }
    public class SingletonBase<T> where T : class, new()
    {
        protected static object sync = new object();
        protected static bool isCreateEnabled = true;
        protected static T instance;
        public static T Instance { get { return StaticCreate(); } }
        public static T Instanced { get { return instance; } }
        protected SingletonBase() { }

        public static T StaticCreate()
        {
            lock (sync)
            {
                if (instance == null && isCreateEnabled)
                {
                    instance = new T();
                    (instance as SingletonBase<T>).Create();
                }
            }

            return instance;
        }

        public static void DestroyInstance()
        {
            if (instance != null)
            {
                (instance as SingletonBase<T>).Destroy();
            }
            instance = null;
        }

        protected virtual void Create() { }
        protected virtual void Destroy() { }
    }

    public class Singleton<T> : SingletonBase<T>, ISingleton where T : class, new()
    {
        public void CallMethod(eSingleton method)
        {
            switch (method)
            {
                case eSingleton.CreateInstance:
                    {
                        StaticCreate();
                    }
                    break;
                case eSingleton.DestroyInstance:
                    {
                        DestroyInstance();
                    }
                    break;
            }
        }
        protected virtual void Initialize() { }
        protected virtual void Release() { }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        protected override void Create()
        {
            isCreateEnabled = false;
            Initialize();
        }

        protected override void Destroy()
        {
            Release();
        }
    }

    public class SingletonContainer<T> : Singleton<T> where T : class, new()
    {
        protected List<ISingleton> m_Childs = new List<ISingleton>();
        protected void AddChild(ISingleton singleton)
        {
            m_Childs.Add(singleton);
        }

        protected override void Release()
        {
            for (int i = m_Childs.Count - 1; i >= 0; --i)
            {
                if (m_Childs[i] != null)
                {
                    m_Childs[i].CallMethod(eSingleton.DestroyInstance);
                }
            }
            m_Childs.Clear();
        }

        protected override void Destroy()
        {
            base.Destroy();
            Release();
        }
    }
}