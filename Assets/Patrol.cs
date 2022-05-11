    // Patrol.cs
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;
    using System.Collections.Generic; //for lists 

    public class Patrol : MonoBehaviour {

        public Transform[] points;
        public float reactionTime = 1;
        public float waitAtPointInterval = 2f;

        public Transform eyePivot;
        public AnimationCurve curve;
        public List<Transform> looks = new List<Transform>(); 

        public enum state {Patrolling, Chasing, Searching};
        public state currentState = state.Patrolling; //default state
        

        private int destPoint = 0;
        private NavMeshAgent agent;
        bool waitingAtPoint = false;

        private AIFoV fov;
        private state lastFrameState;

        IEnumerator wait;
        
        void Start () {
            agent = GetComponent<NavMeshAgent>();
            fov = GetComponent<AIFoV>();

            lastFrameState = currentState;

            wait = WaitAtPatrolPoint();
            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            // agent.autoBraking = false;

            GotoNextPoint();
        }


        void GotoNextPoint() {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination
            destPoint = (destPoint + 1) % points.Length;
            Debug.Log("Going to " + points[destPoint].name);

            currentState = state.Patrolling;
        }

        private float eyesOnPlayerTimer = 0;

        void Patrolling() {
            LookForPlayer();

            // Choose the next destination point when the agent gets close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f) {
                currentState = state.Searching;
            }
        }
        void Chasing() {
            agent.destination = fov.player.position;
            float distance = Vector3.Distance(this.transform.position, fov.player.position);
            //Debug.Log("Distance: " + distance);
            if(distance > fov.sightDistance) {
                currentState = state.Patrolling;
            }
        }
        void Searching() {
            if(!waitingAtPoint) {
                StartCoroutine(wait);
            }
            LookForPlayer();
        }

        void LookForPlayer() {
            if(fov.canSeePlayer == true) {
                eyesOnPlayerTimer += Time.deltaTime;
                if(eyesOnPlayerTimer > reactionTime * .5f) {
                    currentState = state.Chasing;
                    eyesOnPlayerTimer = 0;
                    StopCoroutine(wait);
                    wait = null;
                    eyePivot.rotation = looks[0].rotation;
                    waitingAtPoint = false;
                    
                    return;
                }
            }
            else {
                eyesOnPlayerTimer = 0;
            }
        }

        void Update () {
            switch(currentState) {
                case state.Patrolling: Patrolling(); break;
                case state.Chasing: Chasing(); break;
                case state.Searching: Searching(); break;
            }

            if(lastFrameState != currentState) { 
                Debug.Log("State has changed to " + currentState);
                //Debug.Log("Agent remaining Distance: " + agent.remainingDistance);
            }

            lastFrameState = currentState;

        }

        IEnumerator WaitAtPatrolPoint() {     
        waitingAtPoint = true;  
        // look left and wait 1 second
        // float timer = 0;
        // while (timer < 1){
        //     eyePivot.rotation = Quaternion.Lerp(looks[0].rotation, looks[1].rotation, curve.Evaluate(timer));
        //     timer += (Time.deltaTime * 10);
        //     yield return null;
        // }

        // yield return new WaitForSeconds(1);

        // // look right and wait 1 second
        // timer = 0;
        // while (timer < 1){
        //     eyePivot.rotation = Quaternion.Lerp(looks[1].rotation, looks[2].rotation, curve.Evaluate(timer));
        //     timer += Time.deltaTime;
        //     yield return null;
        // }

        yield return new WaitForSeconds(1);

        // // look forward and go to next point
        // timer = 0;
        // while (timer < 1){
        //     eyePivot.rotation = Quaternion.Lerp(looks[2].rotation, looks[0].rotation, curve.Evaluate(timer));
        //     timer += Time.deltaTime;
        yield return null;
        // }

        GotoNextPoint();
        waitingAtPoint = false;
        }
    }


        
