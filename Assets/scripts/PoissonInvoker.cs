using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FA
{
	public class PoissonInvoker : MonoBehaviour {
		public float _Lamda = 1.0f;

		public float _LeftTime = 0.0f;
		public float _MaxDeltaTime = 10.0f;

		public UnityEvent _Happen;

		// Use this for initialization
		void Start () {
			_LeftTime = Utils.GenPoissonDeltaTimeInterval (_Lamda, _MaxDeltaTime);
		}
		
		// Update is called once per frame
		void Update () {
			if (_LeftTime < 0.0f) {
				_Happen.Invoke ();
				_LeftTime = Utils.GenPoissonDeltaTimeInterval (_Lamda,_MaxDeltaTime);
			}
			_LeftTime -= Time.deltaTime;
		}


        public void SetLamda(float lamda)
        {
            _Lamda = lamda;
        }
	}


}
