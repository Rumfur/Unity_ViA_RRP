using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class AIWinStatus{
    public static bool AIWin = false;
    public static bool restartGame = false;
}

namespace KartGame.KartSystems
{
    public class KartAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        private ArcadeKart arcadeKart;
        private Vector3 targetPosVector;
        public AudioSource engine; 
        public AudioSource sliding;
        public new Animator animation;

        private bool finished = false;
        // private bool accelerate = false;
        // private bool brake = false;
        // private float turnInput = 0;

        private GameObject[] checkpoints;
        private int checkpointIndex = -1;
        private int checkpointCount = 0;

        private Vector3 previousPosition;
        private Quaternion previousRotation;
        private float curSpeed;
        private bool isSliding = false;
        public ParticleSystem slideTrail;

        // Start is called before the first frame update
        void Start()
        {
            arcadeKart = GetComponent<ArcadeKart>();
            //SetTargetPosition(initialTarget.position);
            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            checkpointCount = checkpoints.Length;
            getNewTarget();
            AIWinStatus.AIWin = false;
            AIWinStatus.restartGame = false;
            previousRotation = transform.rotation;
            previousPosition = transform.position;
            sliding.Stop();
            slideTrail.Stop();
        }

        void getNewTarget(){
            checkpointIndex++;
            if (checkpointIndex != checkpointCount) {
                if(checkpoints[checkpointIndex] != null) {
                SetTargetPosition(checkpoints[checkpointIndex].transform.position);
                } else {
                    getNewTarget();
                }
            } else {
                finished = true;
                if(GameObject.FindGameObjectsWithTag("Checkpoint").Length > 0) {
                    AIWinStatus.AIWin = true;
                }
            }
        }

        void checkIfTargetReached(){
            if (Vector3.Distance(transform.position, targetPosVector) < 8f) {
                getNewTarget();
            }
        }

        // Update is called once per frame
        void Update()
        {
            bool canRace = TimeManager.getRaceStatus();
            if (!finished && canRace) { 
                checkIfTargetReached();
                agent.SetDestination(targetPosVector);
            //     bool userDrivingModel = false;
            //     if(userDrivingModel) // code for using driving model
            //     {
            //         Vector3 direction = (targetPosVector - transform.position).normalized;
            //         if (Vector3.Dot(transform.forward, (direction)) < 0)
            //         {
            //             accelerate = false;
            //             brake = true;
            //         } else
            //         {
            //             brake = false;
            //         }
            //         if (accelerate == false && brake == false)
            //         {
            //             accelerate = true;
            //         }

            //         float turnAngle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            //         turnInput = 0;
            //         if (turnAngle > 0)
            //         {
            //             turnInput = 1;
            //         }
            //         if (turnAngle < 0)
            //         {
            //             turnInput = -1;
            //         }
            //         Debug.Log(turnAngle);

            //         arcadeKart.MoveVehicle(accelerate, brake, turnInput);
            //     }
            }
            curSpeed = ((Vector3)(transform.position - previousPosition)).magnitude / Time.deltaTime;
            previousPosition = transform.position;
            if (curSpeed < 5000) {
                engine.pitch = (float)(0.3 + (curSpeed / 10));
                animation.speed = curSpeed;
            }

            if (isSliding == false) {
                if((Mathf.Abs((transform.rotation.x - previousRotation.x)*100000) > 0.001) || (Mathf.Abs((transform.rotation.x - previousRotation.x)*100000) > 0.001)) {
                    isSliding = true;
                    StartCoroutine(playSlidingAudio());
                }
                previousRotation = transform.rotation;
            }
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            this.targetPosVector = targetPosition;
        }

        IEnumerator playSlidingAudio(){
            for (int i = 0; i < 1; i++) {
                sliding.Play();
                slideTrail.Play();
                yield return new WaitForSeconds(0.2f);
            }
            sliding.Stop();
            slideTrail.Stop();
            yield return new WaitForSeconds(0.2f);
            isSliding = false;
        }
    }
}
