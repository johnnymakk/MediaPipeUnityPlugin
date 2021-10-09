using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using UnityEngine;

namespace Mediapipe.Unity.PoseTracking { 
    public class PoseTrackingRotation : MonoBehaviour
    {


        private PoseTrackingSolution _poseTrackingSolution;
        private LandmarkList myLandmarkList;
        private NormalizedLandmarkList myNormalisedLandmarkList;
        private float firstRotationZ = 0f;
		public Transform myGO;
        private enum bodyLandmarks {NOSE, LEFT_EYE_INNER, LEFT_EYE, LEFT_EYE_OUTER, RIGHT_EYE_INNER, RIGHT_EYE, RIGHT_EYE_OUTER, LEFT_EAR, RIGHT_EAR, LEFT_MOUTH, RIGHT_MOUTH, LEFT_SHOULDER, RIGHT_SHOULDER, LEFT_ELBOW, RIGHT_ELBOW, LEFT_WRIST, RIGHT_WRIST, LEFT_PINKY, RIGHT_PINKY, LEFT_INDEX, RIGHT_INDEX, LEFT_THUMB, FIGHT_THUMB, LEFT_HIP, RIGHT_HIP, LEFT_KNEE, RIGHT_KNEE, LEFT_ANKLE, RIGHT_ANKLE, LEFT_HEEL, RIGHT_HEEL, LEFT_FOOT_INDEX, RIGHT_FOOT_INDEX};
        // Start is called before the first frame update
        void Start()
        {
            _poseTrackingSolution = GameObject.FindObjectOfType<PoseTrackingSolution>();
        }

        // Update is called once per frame
        void Update()
        {
            //LandmarkList myLandmarkValues = poseTrackingSolution.value.poseWorldLandmarks;
            myLandmarkList = _poseTrackingSolution.poseWorldLandmarkList;
            myNormalisedLandmarkList = _poseTrackingSolution.poseNormalisedLandmarkList;
            firstRotationZ = myLandmarkList.Landmark[(int)bodyLandmarks.RIGHT_WRIST].X;
			//Pose(myNormalisedLandmarkList.Landmark);
			WristLeft = MapMediapipePosition(myNormalisedLandmarkList.Landmark[(int)bodyLandmarks.LEFT_WRIST]);
			//Debug.Log("tracking values :" + myLandmarkList);
			//Debug.Log("landmark 0 X: " + firstRotationZ);
			//Debug.Log("normalised landmarks: " + myNormalisedLandmarkList);
			Debug.Log("Left Wrist: " + WristLeft.x);
			myGO.position = WristLeft;
        }

        public Vector3 MapMediapipePosition(NormalizedLandmark landmark)
        {
            return new Vector3(landmark.X * 640f, (1f - landmark.Y) * 480f, -320f * landmark.Z);
        }

		/*
		public void Pose(RepeatedField<NormalizedLandmark> landmarks)
		{
			this.ShoulderLeft = this.MapMediapipePosition(landmarks.[(int)bodyLandmarks.LEFT_SHOULDER]);
			this.ElbowLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_ELBOW]);
			this.WristLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_WRIST]);
			this.ShoulderRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_SHOULDER]);
			this.ElbowRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_ELBOW]);
			this.WristRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_WRIST]);
			this.IndexLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_INDEX]);
			this.IndexRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_INDEX]);
			this.HipLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_HIP]);
			this.HipRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_HIP]);
			this.KneeLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_KNEE]);
			this.KneeRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_KNEE]);
			this.AnkleLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_ANKLE]);
			this.AnkleRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_ANKLE]);
			this.HeelLeft = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.LEFT_HEEL]);
			this.HeelRight = this.MapMediapipePosition(landmarks[(int)bodyLandmarks.RIGHT_HEEL]);
			/*	GameObject Right0 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand");
				Right0.transform.position = new Vector3(this.WristRight.x, this.WristRight.y, this.WristRight.z);
				Debug.Log(Right0.transform.position); */
		//}

		// Token: 0x0400015B RID: 347
		public Vector3 ShoulderLeft;

		// Token: 0x0400015C RID: 348
		public Vector3 ShoulderRight;

		// Token: 0x0400015D RID: 349
		public Vector3 ElbowLeft;

		// Token: 0x0400015E RID: 350
		public Vector3 ElbowRight;

		// Token: 0x0400015F RID: 351
		public Vector3 WristLeft;

		// Token: 0x04000160 RID: 352
		public Vector3 WristRight;

		// Token: 0x04000161 RID: 353
		public Vector3 IndexLeft;

		// Token: 0x04000162 RID: 354
		public Vector3 IndexRight;

		// Token: 0x04000163 RID: 355
		public Vector3 HipLeft;

		// Token: 0x04000164 RID: 356
		public Vector3 HipRight;

		// Token: 0x04000165 RID: 357
		public Vector3 KneeLeft;

		// Token: 0x04000166 RID: 358
		public Vector3 KneeRight;

		// Token: 0x04000167 RID: 359
		public Vector3 AnkleLeft;

		// Token: 0x04000168 RID: 360
		public Vector3 AnkleRight;

		// Token: 0x04000169 RID: 361
		public Vector3 HeelLeft;

		// Token: 0x0400016A RID: 362
		public Vector3 HeelRight;
	}

}