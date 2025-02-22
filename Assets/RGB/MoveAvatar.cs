﻿using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class MoveAvatar : MonoBehaviour
{
  // Token: 0x06000142 RID: 322 RVA: 0x00006026 File Offset: 0x00004226
  private void Start()
  {
    this.poseManager = GameObject.Find("PoseManager").GetComponent<PoseManager>();
  }

  // Token: 0x06000143 RID: 323 RVA: 0x0000603D File Offset: 0x0000423D
  private void Update()
  {
  }

  // Token: 0x06000144 RID: 324 RVA: 0x00006040 File Offset: 0x00004240
  public void MoveUpperJoints()
  {
    Debug.Log("moveupperjoints  ");
    if (!this.poseManager.hasPose())
    {
      return;
    }
    Vector3 wristLeft = this.poseManager.GetPose().WristLeft;
    Debug.Log("wrist left x is " + wristLeft.x);
    Vector3 elbowLeft = this.poseManager.GetPose().ElbowLeft;
    Quaternion b = Quaternion.FromToRotation(Vector3.right, elbowLeft - wristLeft);
    this.LeftForearm.rotation = Quaternion.Slerp(this.LeftForearm.rotation, b, 10f * Time.deltaTime);
    Vector3 shoulderLeft = this.poseManager.GetPose().ShoulderLeft;
    b = Quaternion.FromToRotation(Vector3.right, shoulderLeft - elbowLeft);
    this.LeftShoulder.rotation = Quaternion.Slerp(this.LeftShoulder.rotation, b, 10f * Time.deltaTime);
    Vector3 wristRight = this.poseManager.GetPose().WristRight;
    Vector3 elbowRight = this.poseManager.GetPose().ElbowRight;
    b = Quaternion.FromToRotation(Vector3.left, elbowRight - wristRight);
    this.RightForearm.rotation = Quaternion.Slerp(this.RightForearm.rotation, b, 10f * Time.deltaTime);
    Vector3 shoulderRight = this.poseManager.GetPose().ShoulderRight;
    b = Quaternion.FromToRotation(Vector3.left, shoulderRight - elbowRight);
    this.RightShoulder.rotation = Quaternion.Slerp(this.RightShoulder.rotation, b, 10f * Time.deltaTime);
  }

  // Token: 0x06000145 RID: 325 RVA: 0x000061AC File Offset: 0x000043AC
  public void MoveLowerJoints()
  {
    if (!this.poseManager.hasPose())
    {
      return;
    }
    Vector3 hipLeft = this.poseManager.GetPose().HipLeft;
    Vector3 kneeLeft = this.poseManager.GetPose().KneeLeft;
    Vector3 ankleLeft = this.poseManager.GetPose().AnkleLeft;
    Vector3 heelLeft = this.poseManager.GetPose().HeelLeft;
    Quaternion b = Quaternion.FromToRotation(Vector3.up, kneeLeft - ankleLeft);
    this.LeftKnee.rotation = Quaternion.Slerp(this.LeftKnee.rotation, b, 10f * Time.deltaTime);
    b = Quaternion.FromToRotation(Vector3.up, hipLeft - kneeLeft);
    this.LeftHip.rotation = Quaternion.Slerp(this.LeftHip.rotation, b, 10f * Time.deltaTime);
    Vector3 hipRight = this.poseManager.GetPose().HipRight;
    Vector3 kneeRight = this.poseManager.GetPose().KneeRight;
    Vector3 ankleRight = this.poseManager.GetPose().AnkleRight;
    b = Quaternion.FromToRotation(Vector3.up, kneeRight - ankleRight);
    this.RightKnee.rotation = Quaternion.Slerp(this.RightKnee.rotation, b, 10f * Time.deltaTime);
    b = Quaternion.FromToRotation(Vector3.up, hipRight - kneeRight);
    this.RightHip.rotation = Quaternion.Slerp(this.RightHip.rotation, b, 10f * Time.deltaTime);
  }

  // Token: 0x06000146 RID: 326 RVA: 0x00006329 File Offset: 0x00004529
  private void Goodbye()
  {
    //Object.Destroy(base.gameObject);
  }

  // Token: 0x0400012C RID: 300
  private PoseManager poseManager;

  // Token: 0x0400012D RID: 301
  public Transform LeftForearm;

  // Token: 0x0400012E RID: 302
  public Transform LeftShoulder;

  // Token: 0x0400012F RID: 303
  public Transform RightForearm;

  // Token: 0x04000130 RID: 304
  public Transform RightShoulder;

  // Token: 0x04000131 RID: 305
  public Transform LeftHip;

  // Token: 0x04000132 RID: 306
  public Transform LeftKnee;

  // Token: 0x04000133 RID: 307
  public Transform RightHip;

  // Token: 0x04000134 RID: 308
  public Transform RightKnee;
}
