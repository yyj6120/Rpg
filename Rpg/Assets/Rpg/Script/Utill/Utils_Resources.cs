
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    public class Customize_Resources
    {
        private static Dictionary<string, Object> m_ResCache = new Dictionary<string, Object>();

        public static bool IsContains(string path)
        {
            return m_ResCache.ContainsKey(path);
        }

        public static Object Load(string path)
        {
            Object value = null;
            if (!m_ResCache.TryGetValue(path, out value))
            {
                value = Resources.Load(path);
                m_ResCache.Add(path, value);
            }
            return value;

        }

        public static Object Instantiate(Object original)
        {
            if (original != null)
                return Object.Instantiate(original);
            else
                return null;

        }

        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
        {
            return Object.Instantiate(original, position, rotation);
        }

        public static Object Instantiate(string path)
        {
            Object original = Load(path);
            if (original != null)
            {
                return Object.Instantiate(original);
            }
            return null;
        }

        public static Object Instantiate(string path, Vector3 position, Quaternion rotation)
        {
            Object original = Load(path);
            if (original != null)
            {
                return Object.Instantiate(original, position, rotation);
            }
            return null;

        }

        public static void ClearCache()
        {
            m_ResCache.Clear();
        }
    }
    public class SpriteSheetManager
    {
        // 스프라이트 시트에 포함된 스프라이트를 캐시하는 딕셔너리
        private static Dictionary<string, Dictionary<string, Sprite>> spriteSheets = new Dictionary<string, Dictionary<string, Sprite>>();
        // 스프라이트 시트에 포함된 스프라이트를 읽어 들여 캐시하는 메서드
        public static void Load(string path)
        {
            if (!spriteSheets.ContainsKey(path))
            {
                spriteSheets.Add(path, new Dictionary<string, Sprite>());
            }
            // 스프라이트를 읽어 들여 이름과 관련지어서 캐시한다
            Sprite[] sprites = Resources.LoadAll<Sprite>(path);
            foreach (Sprite sprite in sprites)
            {
                if (!spriteSheets[path].ContainsKey(sprite.name))
                {
                    spriteSheets[path].Add(sprite.name, sprite);
                }
            }
        }
        // 스프라이트 이름을 통해 스프라이트 시트에 포함된 스프라이트를 반환리턴하는 메서드
        public static Sprite GetSpriteByName(string path, string name)
        {
            if (spriteSheets.ContainsKey(path) && spriteSheets[path].ContainsKey(name))
            {
                return spriteSheets[path][name];
            }
            return null;
        }
    }
}