using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FA
{
	public class Utils {

		static public float Remap(
			float value,
			float inputMin, float inputMax, 
			float outputMin, float outputMax)
		{
			float value01 = 
				(value - inputMin) / (inputMax - inputMin);
			float valueOut = 
				(outputMax - outputMin) * value01 + outputMin;
			return valueOut;
		}

		static public float Repeat(float value, float period, float phase)
		{
			float value1 = value - phase;
			float value2 = Mathf.Repeat (value1, period);
			float value3 = value2 + phase;
			return value3;
		
		}

		static public int Repeat(int t, int period)
		{
			int outVal = Mathf.RoundToInt (
				Mathf.Repeat ((float)t, (float)period));
			return outVal;
		}


		static public GameObject ChooseGBByName(List<GameObject> gbs, string name)
		{
			GameObject gb = null;
			foreach (var g in gbs) {
				if (g.name == name) {
					gb = g;
					break;
				}
			}
			return gb;
		}

		static public float GenPoissonDeltaTimeInterval(float lamda, float maxTime)
		{
			bool bGet = false;
			float x = maxTime;
			int cnt = 0;
			do {
				x = Random.Range (0.0f, maxTime);
				float prob =  Mathf.Exp (-lamda * x);

				float y = Random.value;

				bGet = (y<prob);
				//Debug.Log("prob:" + prob + " x:" + x + " y:" + y);

				if(bGet)
				{
					break;
				}

				cnt ++;
				if(cnt>100)
				{
					break;
				}
			} while(true);

			return x;
		}

		public static Vector3 MousePos2ZPlane(Camera cam, float z)
		{
			Ray mouseRay = cam.ScreenPointToRay (Input.mousePosition);
			float zDist = z - cam.transform.position.z;
			Vector3 mouseOnZ = mouseRay.origin + zDist * mouseRay.direction;

			return mouseOnZ;
		}


        static public Vector3 GetVector3AtList(List<Vector3> List, float idf)
        {
            if (List.Count < 1)
            {
                return Vector3.negativeInfinity;
            }
            if (idf < 0.0f || idf > List.Count - 1.0f)
            {
                return Vector3.negativeInfinity;
            }

            int id0 = Mathf.FloorToInt(idf);

            int id1 = id0 + 1;


            if (id1 >= List.Count)
            {
                id0--;
                id1--;
            }
            Vector3 pos0 = List[id0];
            Vector3 pos1 = List[id1];

            float t = idf - (float)id0;

            Vector3 post = Vector3.Lerp(pos0, pos1, t);

            return post;
        }


    }
}
