using System.Collections.Generic;
using System.Collections;
using _BaseFeatures.Audios;
using _Main.Scripts.Definition.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DogAndCat;

namespace DOG.Extension
{
    public static class ExtensionMethod
    {
        #region Transform Extention

        public static Transform FindDeepChilds(this Transform transf, string key)
        {
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(transf);
            Transform temp = null;
            while (queue.Count > 0)
            {
                temp = queue.Peek().Find(key);
                if (temp != null)
                {
                    queue.Clear();
                    return temp;
                }
                else
                {
                    foreach (Transform item in queue.Dequeue())
                    {
                        queue.Enqueue(item);
                    }
                }
            }
            Debug.LogErrorFormat("Error! Not found object with name \"{0}\" - is child of object \"{1}\"", key, transf.name);
            return null;
        }
        public static Transform FindDeepChilds(this Transform transf, params string[] keys)
        {
            Transform _transform = transf;
            for (int i = 0; i < keys.Length; i++)
            {
                _transform = _transform.FindDeepChilds(keys[i]);
                if (_transform == null) return null;
            }
            return _transform;
        }

        public static TKey FindDeepChilds<TKey>(this Transform transf, string key) where TKey : Component
        {
            return transf.FindDeepChilds(key).GetComponent<TKey>();
        }
        public static TKey FindDeepChilds<TKey>(this Transform transf, params string[] keys)
        {
            return transf.FindDeepChilds(keys).GetComponent<TKey>();
        }
        public static GameObject FindDeepChilds(this GameObject gameObj, params string[] keys)
        {
            return gameObj.transform.FindDeepChilds(keys).gameObject;
        }
        public static TKey FindDeepChilds<TKey>(this GameObject gameObj, params string[] keys)
        {
            return gameObj.FindDeepChilds(keys).GetComponent<TKey>();
        }

        public static T FindComponent<T>(this Transform root, params string[] keys) where T : Component
            => root.FindDeepChilds<T>(keys);
        public static Transform FindTransform<T>(this T root, params string[] keys) where T : Component
            => root.transform.FindDeepChilds(keys);

        #endregion

        #region UnityAction Extension

        public static void TryInvoke(this UnityAction action)
        {
            if (action != null)
                action.Invoke();
        }
        public static void TryInvoke<T>(this UnityAction<T> action, T value)
        {
            if (action != null) action.Invoke(value);
        }
        #endregion

        #region UnityEngine UI Extension

        public static void SetOnClick(this Button bt, UnityAction call)
        {
            bt.onClick.RemoveAllListeners();
            bt.onClick.AddListener(call);
        }

        public static void SetAction(this Button button, UnityAction action, SoundKey soundKey = SoundKey.Click)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                action.Invoke();
                if (soundKey != SoundKey.None)
                    switch (soundKey)
                    {
                        case SoundKey.Click:
                            AudioUtilities.PlaySound(GameManager.AudioData.GetClip(soundKey), 1, 0.8f, 1.2f);
                            break;
                        default:
                            AudioUtilities.PlaySound(GameManager.AudioData.GetClip(soundKey));
                            break;
                    }
            });
        }

        public static void SetText(this Button button, object agr0)
        {
            button.GetComponentInChildren<Text>().SetText(agr0);
        }
        public static void SetText(this Button button, string _format, params object[] args)
        {
            button.GetComponentInChildren<Text>().SetText(_format, args);
        }

        public static void Set(this TextMeshProUGUI _tmp, string str)
        {
            _tmp.text = str;
        }
        public static void Set(this TextMeshProUGUI _tmp, object obj)
        {
            _tmp.text = string.Format("{0}", obj);
        }
        public static void Set(this TextMeshProUGUI _tmp, string _format, params object[] args)
        {
            _tmp.text = string.Format(_format, args);
        }
        public static void SetText(this Text _txt, object obj)
        {
            _txt.text = string.Format("{0}", obj);
        }
        public static void SetText(this Text _txt, string _format, params object[] args)
        {
            _txt.text = (string.Format(_format, args));
        }
        public static Vector3 GetPosition<T>(this T obj) where T : Component
        {
            return obj.transform.position;
        }
        public static Vector2 GetAnchoredPosition<T>(this T obj) where T : Component
        {
            return obj.GetRectTransform().anchoredPosition;
        }
        #endregion

        public static void SetEnable<T>(this T component) where T : MonoBehaviour
        {
            component.enabled = true;
        }
        public static void SetDisable<T>(this T component) where T : MonoBehaviour
        {
            component.enabled = false;
        }
        public static void TryEnable<T>(this T component) where T : MonoBehaviour
        {
            if (component != null && !component.enabled)
                component.enabled = true;
        }
        public static void TryDisable<T>(this T component) where T : MonoBehaviour
        {
            if (component != null)
                component.enabled = false;
        }

        public static void SetActive<T>(this T obj, bool enable) where T : Component
            => obj.gameObject.SetActive(enable);
        public static void onActive<T>(this T obj) where T : Component
            => obj.gameObject.SetActive(true);
        public static void onDeactive<T>(this T obj) where T : Component
            => obj.gameObject.SetActive(false);
      
        public static RectTransform GetRectTransform<T>(this T component) where T : Component
        {
            return component.GetComponent<RectTransform>();
        }
        public static RectTransform GetRectTransform(this GameObject component)
        {
            return component.GetComponent<RectTransform>();
        }

        public static int Abs(this int value)
        {
            return value >= 0 ? value : -value;
        }
        public static float Abs(this float value)
        {
            return value >= 0 ? value : -value;
        }
        public static float Claim(this float value, float minValue = 0, float maxValue = 1)
        {
            if (value < minValue) value = minValue;
            if (value > maxValue) value = maxValue;
            return value;
        }
        public static int Claim(this int value, int minValue = 0, int maxValue = 1)
        {
            if (value < minValue) value = minValue;
            if (value > maxValue) value = maxValue;
            return value;
        }

        #region Array Methods

        public static T[] FindAll<T>(this T[] array, System.Predicate<T> match)
        {
            return System.Array.FindAll(array, match);
        }

        #endregion

        #region List Extension
        public static List<T> ToList<T>(this T obj)
        {
            List<T> list = new List<T>();
            list.Add(obj);
            return list;
        }
        public static List<T> Add<T>(this List<T> list, T obj, int position)
        {
            int lastPosition = list.Count;
            list.Add(obj);
            for (int i = position; i <= lastPosition; i++)
            {
                T temp = list[i];
                list[i] = list[lastPosition];
                list[lastPosition] = temp;
            }
            return list;
        }
        #endregion

        public static Vector3 GetRandomPointInsideCollider(this BoxCollider boxCollider)
        {
            Vector3 extents = boxCollider.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                0.1f,
                Random.Range(-extents.z, extents.z)
            );

            return boxCollider.transform.TransformPoint(point);
        }

        public static Vector3 GetRandomTerrainItemCollider(this BoxCollider boxCollider)
        {
            Vector3 extents = boxCollider.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                0.1f
            );

            
            return boxCollider.transform.TransformPoint(point);
        }

        public static IEnumerator MoveTo(this RectTransform rect, Vector2 _posStart, Vector2 _posTarget)
        {
            rect.anchoredPosition = _posStart;
            while (rect.anchoredPosition != _posTarget)
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, _posTarget, 5);
                yield return 0;
            }
        }

        public static IEnumerator ScaleTo(this Transform rect, Vector3 _scaleStart, Vector3 _scaleTarget)
        {
            rect.localScale = _scaleStart;
            while (rect.localScale != _scaleTarget)
            {
                rect.localScale = Vector3.MoveTowards(rect.localScale, _scaleTarget, 0.1f);
                yield return 0;
            }
        }

    }
}