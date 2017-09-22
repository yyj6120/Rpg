
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    using GameObjectList = List<GameObject>;
    #region 커스텀오브젝트풀
    public class CustomObjectPool
    {
        public Transform Parent = null;

        protected GameObject m_BaseObject;
        protected GameObjectList m_ReUsePool = new GameObjectList();
        protected Dictionary<int, GameObject> m_DictionaryBaseObject = new Dictionary<int, GameObject>();
        protected Dictionary<int, GameObjectList> m_DicReUsePool = new Dictionary<int, GameObjectList>();
        protected Dictionary<int, GameObject> m_UsePool = new Dictionary<int, GameObject>();
        public Dictionary<int, GameObject> UsePoolObject
        {
            get{ return m_UsePool; }
        }
        public CustomObjectPool()
        {
           
        }
        public CustomObjectPool(GameObject original = null, Transform parent = null)
        {
            Register(original,parent);
        }
        public CustomObjectPool(string _key , GameObject original = null, Transform parent = null)
        {
            Register( _key , original , parent);
        }
        public int UsePoolCount
        {
            get { return m_UsePool.Count; }
        }

        public void Register(GameObject go , Transform _parent)
        {
            m_BaseObject = go;
            Parent = _parent;
        }
        public void Register(string _name , GameObject go , Transform _parent)
        {
            int _key = _name.GetHashCode();
            m_DictionaryBaseObject[_key] = go;
            m_DicReUsePool[_key] = new GameObjectList();
            Parent = _parent;
        }
        public void Destroy(bool bUseObject)
        {
            m_BaseObject = null;
            if (bUseObject)
            {
                foreach (var it in m_UsePool)
                {
                    GameObject.Destroy(it.Value);
                }
                m_UsePool.Clear();
            }
            else
            {
                int count = m_ReUsePool.Count;
                for (int i = 0; i < count; ++i)
                {
                    GameObject.Destroy(m_ReUsePool[i]);
                }
                m_ReUsePool.Clear();
            }
        }
        protected virtual GameObject SpawnObject()
        {
            if (m_BaseObject == null)
            {
                Debug.LogError("등록된 오브젝트가 없습니다.");
                return new GameObject("GO");
            }
            return Customize_Resources.Instantiate(m_BaseObject) as GameObject;
        }
        protected virtual GameObject SpawnObject(string _name)
        {
            int _key = _name.GetHashCode();
            if (m_DictionaryBaseObject[_key] == null)
            {
                Debug.LogError("등록된 오브젝트가 없습니다.");
                return new GameObject("GO");
            }
            return Customize_Resources.Instantiate(m_DictionaryBaseObject[_key]) as GameObject;
        }
        public virtual GameObject GetInstance()
        {
#if UNITY_EDITOR
            if (m_BaseObject == null)
            {
                Debug.LogError("등록된 오브젝트가 없습니다.");
                return null;
            }
#endif
            GameObject go;
            if (m_ReUsePool.Count == 0)
            {
                go = SpawnObject();
                go.transform.parent = Parent;
            }
            else
            {
                int instIndex = m_ReUsePool.Count - 1;
                go = m_ReUsePool[instIndex];
                go.SetActive(true);
                m_ReUsePool.RemoveAt(instIndex);
            }
            m_UsePool.Add(go.GetInstanceID(), go);
            return go;
        }
        public virtual GameObject GetInstance(string _name)
        {
            int _key = _name.GetHashCode();
#if UNITY_EDITOR
            if (m_DictionaryBaseObject[_key] == null)
            {
                Debug.LogError("등록된 오브젝트가 없습니다.");
                return null;
            }
#endif
            GameObject go;
            if (m_DicReUsePool[_key].Count == 0)
            {
                go = SpawnObject(_name);
                go.transform.parent = Parent;
            }
            else
            {
                int instIndex = m_DicReUsePool[_key].Count - 1;
                go = m_DicReUsePool[_key][instIndex];
                go.SetActive(true);
                m_DicReUsePool[_key].RemoveAt(instIndex);
            }
            m_UsePool.Add(go.GetInstanceID(), go);
            return go;
        }
        public void UnUseInsert(GameObject go)
        {
            if (go == null) return;
            int ObjectHeshKey = go.GetInstanceID();
            if (m_UsePool.ContainsKey(ObjectHeshKey) == false) return;

            go.SetActive(false);
            m_UsePool.Remove(ObjectHeshKey);
            m_ReUsePool.Add(go);
        }
        public void UnUseInsert(string _name, GameObject go)
        {
            if (go == null)
            {
                return;
            }
            int _key = _name.GetHashCode();
            int ObjectHeshKey = go.GetInstanceID();
            if (m_UsePool.ContainsKey(ObjectHeshKey) == false)
            {
                return;
            }     
            go.SetActive(false);
            m_UsePool.Remove(ObjectHeshKey);
            m_DicReUsePool[_key].Add(go);
        }
        public IEnumerator UnUseInsert(string _name, GameObject go , float _time)
        {
            yield return new WaitForSeconds(_time);
            UnUseInsert(_name, go);
        }
    }
    #endregion
}