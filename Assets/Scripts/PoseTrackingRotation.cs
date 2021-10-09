using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using UnityEngine;

namespace Mediapipe.Unity.PoseTracking
{
  public class PoseTrackingRotation : MonoBehaviour
  {


    private PoseTrackingSolution _poseTrackingSolution;
    private LandmarkList _myLandmarkList;
    private NormalizedLandmarkList _myNormalisedLandmarkList;
    private float _firstRotationZ = 0f;
    public Transform myGO;
    private enum BodyLandmarks { Nose, Left_eye_inner, Left_eye, Left_eye_outer, Right_eye_inner, Right_eye, Right_eye_outer, Left_ear, Right_ear, Left_mouth, Right_mouth, Left_shoulder, Right_shoulder, Left_elbow, Right_elbow, Left_wrist, Right_wrist, Left_pinky, Right_pinky, Left_index, Right_index, Left_thumb, Right_thumb, Left_hip, Right_hip, Left_knee, Right_knee, Left_ankle, Right_ankle, Left_heel, Right_heel, Left_foot_index, Right_foot_index };
    private enum BodyPosName { Left_shoulder, Right_shoulder, Left_elbow, Right_elbow, Left_wrist, Right_wrist, Left_index, Right_index, Left_hip, Right_hip, Left_knee, Right_knee, Left_ankle, Right_ankle, Left_heel, Right_heel };
    private Vector3[] bodyPositions;
    // Start is called before the first frame update
    void Start()
    {
      _poseTrackingSolution = GameObject.FindObjectOfType<PoseTrackingSolution>();
      bodyPositions = new Vector3[16];
    }

    // Update is called once per frame
    void Update()
    {
      //LandmarkList myLandmarkValues = poseTrackingSolution.value.poseWorldLandmarks;
      _myLandmarkList = _poseTrackingSolution.poseWorldLandmarkList;
      _myNormalisedLandmarkList = _poseTrackingSolution.poseNormalisedLandmarkList;
      _firstRotationZ = _myLandmarkList.Landmark[(int)BodyLandmarks.Right_wrist].X;
      //Pose(myNormalisedLandmarkList.Landmark);
      //bodyPositions[(int)bodyPosName.WristLeft] = MapMediapipePosition(myNormalisedLandmarkList.Landmark[(int)bodyLandmarks.LEFT_WRIST]);
      bodyPositions[(int)BodyPosName.Left_wrist].x = _myLandmarkList.Landmark[(int)BodyLandmarks.Left_wrist].X;
      bodyPositions[(int)BodyPosName.Left_wrist].y = _myLandmarkList.Landmark[(int)BodyLandmarks.Left_wrist].Y;
      bodyPositions[(int)BodyPosName.Left_wrist].z = _myLandmarkList.Landmark[(int)BodyLandmarks.Left_wrist].Z;
      GetBodyPositionsFromLandmarks();
      //int enumNum = (int)bodyPosName.Left_wrist;
      //Debug.Log("tracking values :" + myLandmarkList);
      //Debug.Log("landmark 0 X: " + firstRotationZ);
      //Debug.Log("normalised landmarks: " + myNormalisedLandmarkList);
      //Debug.Log("Left Wrist: " + bodyPositions[(int)bodyPosName.Left_wrist].x);
      myGO.position = bodyPositions[(int)BodyPosName.Left_wrist];
    }

    private Vector3 MapMediapipePosition(NormalizedLandmark landmark)
    {
      return new Vector3(landmark.X * 640f, (1f - landmark.Y) * 480f, -320f * landmark.Z);
    }

    private void GetBodyPositionsFromLandmarks()
    {

      for (int i = 0; i < 16; i++)
      {
        string bodyPart = Enum.GetName(typeof(BodyPosName), i);
        Debug.Log("bodyPosName: " + bodyPart);
        var index = (BodyLandmarks)Enum.Parse(typeof(BodyLandmarks), bodyPart);
        //Debug.Log("index: " + (int)index);
        bodyPositions[i].x = _myLandmarkList.Landmark[(int)index].X;
        bodyPositions[i].y = _myLandmarkList.Landmark[(int)index].Y;
        bodyPositions[i].z = _myLandmarkList.Landmark[(int)index].Z;
      }
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
    /*
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
    */
  }

}
