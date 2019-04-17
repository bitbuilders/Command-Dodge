using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T m_instance = null;

    static public T Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<T>();

                if (!m_instance)
                {
                    GameObject go = new GameObject("Generated [" + typeof(T).ToString() + "] Singleton");
                    m_instance = go.AddComponent<T>();
                }
            }

            return m_instance;
        }
    }
}
