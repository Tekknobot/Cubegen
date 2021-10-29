using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    List<TileScript> selectableTiles = new List<TileScript>();
    GameObject[] tiles;

    Stack<TileScript> path = new Stack<TileScript>();
    TileScript currentTile;

    public bool moving = false;
    public int move = 3;
    public float jumpheight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 14.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    void Start() {
        Init();
    }

    protected void Init() {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile() {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public TileScript GetTargetTile(GameObject target) {
        RaycastHit hit;
        TileScript tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)) {
            tile = hit.collider.GetComponent<TileScript>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists() {
        //tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles) {
            TileScript t = tile.GetComponent<TileScript>();
            t.FindNeighbors(jumpheight);
        }
    }

    public void FindSelectableTiles() {
        ComputeAdjacencyLists();
        GetCurrentTile();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave as null

        while (process.Count > 0 ) {
            TileScript t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move) {
                foreach (TileScript tile in t.adjacencyList) {
                    if (!tile.visited) {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(TileScript tile) {
        path.Clear();
        tile.target = true;
        moving = true;

        TileScript next = tile;
        while (next != null) {
            path.Push(next);
            next = next.parent;
        }
    }       

    public void Move() {
        if (path.Count > 0) {
            TileScript t = path.Peek();
            Vector3 target = t.transform.position;

            //Calculate the unit's position on top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f) {

                bool jump = transform.position.y != target.y;

                if (jump) {
                    Jump(target);
                }
                else {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                //Locomotion
                //transform.forward = heading;
                Animator animator = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMove>().moveAnimation;
                transform.position += velocity * Time.deltaTime;
            }
            else {
                //Tile center reached
                transform.position = target;
                transform.rotation = Quaternion.Euler(45, -45, 0);
                Animator animator = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMove>().idleAnimation;
                path.Pop();
            }
        }
        else {
            RemoveSelectableTiles();
            moving = false;
        }
    }

    protected void RemoveSelectableTiles() {
        if (currentTile != null) {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (TileScript tile in selectableTiles) {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target) {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity() {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target) {
        if (fallingDown) {
            FallDownward(target);
        }
        else if (jumpingUp) {
            JumpUpward(target);
        }
        else if (movingEdge) {
            MoveToEdge();
        }
        else {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target) {
        float targetY = target.y;
        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY) {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY = transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }

    void FallDownward(Vector3 target) {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y) {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target) {
        velocity += Physics.gravity * Time.deltaTime;

        if ( transform.position.y > target.y) {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge() {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f) {
            SetHorizontalVelocity();
        }
        else {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }
}
