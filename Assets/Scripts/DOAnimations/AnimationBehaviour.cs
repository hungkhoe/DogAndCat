using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using _BaseFeatures.DOAnimations.Basic;

namespace _BaseFeatures.DOAnimations
{
    public class AnimationBehaviour : MonoBehaviour
    {
        private static AnimationBehaviour m_instance;

        public static AnimationBehaviour Instance => m_instance ??= CreateSingleton();

        public event UnityAction UpdateEvent;
        private readonly Dictionary<int, List<BaseAnimation>> _animationSaved = new();
        private readonly List<int> _keyDontDestroy = new ();
        public static AnimationBehaviour CreateSingleton()
            => new GameObject("DOAnimation", typeof(AnimationBehaviour)).GetComponent<AnimationBehaviour>();

        public void SetListener(int id, BaseAnimation tween)
        {
            if (_animationSaved.ContainsKey(id))
            {
                _animationSaved[id].Add(tween);
            }
            else
            {
                _animationSaved.Add(id, new List<BaseAnimation>() { tween });
            }
        }
        public void KillAnimation(int id)
        {
            if (_animationSaved.ContainsKey(id))
            {
                for (int i = 0; i < _animationSaved[id].Count; i++)
                {
                    _animationSaved[id][i].ReleaseUpdateOnBehaviour();
                }
                _animationSaved.Remove(id);
            }
        }

        public void CleanUp(int id)
        {
            if (_animationSaved.ContainsKey(id))
            {
                for (int i = 0; i < _animationSaved[id].Count; i++)
                {
                    if (!_animationSaved[id][i].IsPlaying)
                    {
                        _animationSaved[id][i].ReleaseUpdateOnBehaviour();
                        _animationSaved[id].RemoveAt(i);
                    }
                }
                if (_animationSaved[id].Count == 0)
                {
                    _animationSaved.Remove(id);
                }
            }
        }
        public void KillAll()
        {
            if (_keyDontDestroy.Count > 0)
            {
                List<int> keyRemoved = new List<int>();
                foreach (var item in _animationSaved)
                {
                    if (_keyDontDestroy.Contains(item.Key))
                    {
                        continue;
                    }
                    else
                    {
                        keyRemoved.Add(item.Key);
                        foreach (var t in item.Value)
                        {
                            t.ReleaseUpdateOnBehaviour();
                        }
                    }
                }
                foreach (int key in keyRemoved)
                {
                    _animationSaved.Remove(key);
                }
            }
            else
            {
                foreach (var item in _animationSaved)
                {
                    foreach (var t in item.Value)
                    {
                        t.ReleaseUpdateOnBehaviour();
                    }
                }
                _animationSaved.Clear();
            }
        }

        public void SetDontDestroy(int id)
        {
            if (!_keyDontDestroy.Contains(id))
            {
                _keyDontDestroy.Add(id);
            }
        }

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
                SceneManager.sceneUnloaded += OnSceneUnload;
                DontDestroyOnLoad(gameObject);
                Debug.Log("Awake AnimationUIManager");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void OnSceneUnload(Scene scene)
        {
            Time.timeScale = 1;
            KillAll();
        }
        public void Update()
        {
            UpdateEvent?.Invoke();
        }
    }
}
