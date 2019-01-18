using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class UnitMovement : MonoBehaviour
{
    public bool unitTurn = false;
    public bool moving = false;
    public int moveRange = 5;
    public float moveSpeed = 2;

    enum SelectedObj{none,teammate,enemy,tile};
    SelectedObj currentSelectedObj = SelectedObj.none;

    List<Tile> selectableTiles = new List<Tile>();
    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    protected bool alredyAttack = false;
    protected bool alredyMoved = false;

    protected bool selectableTilesFinded = false;
    protected CharacterAnimatorController CharacterAnimController;
    protected ZombieAnimatorController ZombieAnimaController;
    protected Weapon Weapon;

    [HideInInspector]
    public Tile actualTargetTile;

    protected void Initialization()
    {
        halfHeight = GetComponent<Collider>().bounds.extents.y; //Przypisanie wysokosci jednostki nad ziemia do zmiennej 
        CenterPosition();
        CharacterAnimController = GetComponentInChildren<CharacterAnimatorController>();
        ZombieAnimaController = GetComponentInChildren<ZombieAnimatorController>();
        Weapon = GameObject.FindGameObjectWithTag("ActivePlayer").GetComponent<Weapon>();
        TurnManager.AddUnit(this); //Dodanie jednostki
    }

    void CenterPosition() //Wysrodkowuje jednostke na polu na ktorym sie znajduje
    {
        Tile current = GetTargetTile(gameObject);
        Vector3 tilePosition = current.transform.position;
        tilePosition.y += halfHeight + current.GetComponent<Collider>().bounds.extents.y;
        transform.position = tilePosition;
    }

    public void GetCurrentTile() //Aktualizacja pola na ktorym znajduje sie jednostka
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target) //Zwraca pole ktore jest celem poruszjacej sie jednostki
    {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(Tile target) //Oblicza sasiednie plytki kazdej z plytek
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(target);
        }
    }

    public void FindSelectableTiles() //Oblicza plytki po ktorych moze poruszac sie jednostka
    {
        ComputeAdjacencyLists(null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();
        TileCollision collision;

        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            collision = t.GetComponent<TileCollision>();
            if (!collision.collisionWithObstacles)
            {
                if (t.transform.position.y == 0)
                {
                    selectableTiles.Add(t);
                    t.selectable = true;
                    if (tag == "Player")
                    {
                        if (t.GetComponent<Renderer>().material.color != Color.blue)
                            if (t.GetComponent<Renderer>().material.color != Color.red)
                                t.GetComponent<Renderer>().material.color = Color.red;
                        if (currentTile.GetComponent<Renderer>().material.color != Color.white)
                            currentTile.GetComponent<Renderer>().material.color = Color.white;
                    }

                    if (t.distance < moveRange)
                    {
                        foreach (Tile tile in t.adjacencyList)
                        {
                            if (!tile.visited)
                            {
                                tile.parent = t;
                                tile.visited = true;
                                tile.distance = 1 + t.distance;
                                process.Enqueue(tile);
                            }
                        }
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile) //Oblicza plytki na drodze do docelowej plytki
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move() //Ruch jednoski po mapie po wybraniu celu
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //Oblicza pozycje jednostki na gorze docelowej plytki(bloczka)
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizotalVelocity();

                //Poruszanie
                if(tag == "Player")
                    transform.forward = heading;
                else
                    transform.forward = heading * -1;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Docelowa plytka osiagnieta
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
            setHeadingToTarget();
            alredyMoved = true;
        }
    }

    protected void setHeadingToTarget()
    {
        Vector3 target = FindNearestTarget().transform.position;
        CalculateHeading(target);
        if (tag == "Player")
            transform.forward = heading;
        else
            transform.forward = heading * -1;
    }

    protected void RemoveSelectableTiles() //Czysci tablice plytek po ktorych moze poruszac sie jednostka
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
            tile.GetComponent<Renderer>().material.color = Color.white;
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target) // Oblicza zwrot jednostki w strone w ktora sie porusza
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizotalVelocity() //Ustawia wektor predkosc poruszania jednostki
    {
        velocity = heading * moveSpeed;
    }

    protected GameObject FindNearestTarget()
    {
        GameObject[] targets = null;

        if (tag == "Player")
            targets = GameObject.FindGameObjectsWithTag("NPC");
        if (tag == "NPC")
            targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            if (obj.GetComponent<BaseStats>().HP > 0)
            {
                float d = Vector3.Distance(transform.position, obj.transform.position);

                if (d < distance)
                {
                    distance = d;
                    nearest = obj;
                }
            }
        }

        return nearest;
    }

    protected Tile FindLowestF(List<Tile> list) //Tworzy liste plytek o najmniejszym koszcie F 
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t) //Znajduje ostatnia plytke
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= moveRange)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= moveRange; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(Tile target) //Oblicza droge dla NPC do docelowej plytki
    {
        ComputeAdjacencyLists(target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent = ??
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }

        //todo - what do you do if there is no path to the target tile?
        Debug.Log("Path not found");
    }

    protected void UnitIsAlive(UnitMovement unit)
    {
        if (unit.GetComponent<BaseStats>().HP <= 0)
        {
            if (unit.tag == "Player")
                unit.GetComponentInChildren<CharacterAnimatorController>().lateDeathAnimation = true;
            if (unit.tag == "NPC")
                unit.GetComponentInChildren<ZombieAnimatorController>().lateDeathAnimation = true;
            TurnManager.RemoveUnit(unit);
        }
    }

    protected void CheckMouse() {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    RunIfMoveAction(hit);
                }
                else if (EnemyTag(hit) && !alredyAttack)
                {
                    RunIfAttackAction(hit);
                }
            }
        }
    }

    private void RunIfAttackAction(RaycastHit hit) {

        if (hit.collider.GetComponent<BaseStats>().HP > 0)
        {
            removeSelectedTileByMouse();

            RingSelectedEnemy.SetActiveRing();

            heading = hit.collider.transform.position - transform.position;
            heading.Normalize();
            transform.forward = heading;

            if (currentSelectedObj == SelectedObj.enemy && RingInTheSamePosition(hit))
            {
                currentSelectedObj = SelectedObj.none;
                RingSelectedEnemy.SetDeactiveRing();
                AttackEnemy(hit);
            }
            currentSelectedObj = SelectedObj.enemy;

            if (hit.collider.gameObject.transform.position != RingSelectedEnemy.GetRingPosition())
            {
                RingSelectedEnemy.GetEnemyPosition(hit.collider.GetComponent<UnitMovement>());
            }
        }
    }

    private void RunIfMoveAction(RaycastHit hit) {

        RingSelectedEnemy.SetDeactiveRing();

        Tile t = hit.collider.GetComponent<Tile>();

        if (t.selectable)
        {
            if (currentSelectedObj == SelectedObj.tile && t.GetComponent<Renderer>().material.color == Color.blue)
            {
                t.GetComponent<Renderer>().material.color = Color.green;
                currentSelectedObj = SelectedObj.none;
                MoveToTile(t);
            }
            currentSelectedObj = SelectedObj.tile;

            if (t.GetComponent<Renderer>().material.color != Color.blue && t.GetComponent<Renderer>().material.color != Color.green)
            {
                foreach (Tile tile in selectableTiles)
                {
                    if (tile.GetComponent<Renderer>().material.color == Color.blue)
                    {
                        tile.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
                t.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    private void removeSelectedTileByMouse()
    {
        if (!alredyMoved)
            foreach (Tile tile in selectableTiles)
            {
                if (tile.GetComponent<Renderer>().material.color == Color.blue)
                    tile.GetComponent<Renderer>().material.color = Color.red;
            }
    }

    private bool RingInTheSamePosition(RaycastHit hit)
    {
        if (hit.collider.gameObject.transform.position.x == RingSelectedEnemy.GetRingPosition().x)
            if (hit.collider.gameObject.transform.position.z == RingSelectedEnemy.GetRingPosition().z)
                if (RingSelectedEnemy.GetRingPosition().y > 0)
                    return true;

        return false;
    }

    private bool EnemyTag(RaycastHit hit)
    {
        foreach (String tag in TurnManager.GetTeamsTagsList())
        {
            if (tag != TurnManager.GetCurrentTeam())
            {
                if (hit.collider.tag == tag)
                    return true;
            }
        }
        return false;
    }

    private void AttackEnemy(RaycastHit hit)
    {
        Vector3 distance = new Vector3();
        distance = hit.collider.transform.position - transform.position;
        if (Mathf.Abs(distance.magnitude)<=this.GetComponent<FieldOfView>().viewRadius)
        {
            if (Mathf.Abs(distance.magnitude) >= 3)
            {
                Weapon.setGunVisible(true);
                hit.collider.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().RangeAtack;
                CharacterAnimController.isShooting = true;
            }
            else if(Mathf.Abs(distance.magnitude) < 3)
            {
                hit.collider.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().MeleAtack;
                CharacterAnimController.isPunching = true;
            }

            alredyAttack = true;
        }

        UnitMovement unit = hit.collider.GetComponent<UnitMovement>();
        if (unit.GetComponent<BaseStats>().HP <= 0)
        {
            RingSelectedEnemy.SetDeactiveRing();
            UnitIsAlive(unit);
        }
        else if (alredyAttack)
            unit.GetComponentInChildren<ZombieAnimatorController>().lateHitAnimation = true;
    }

    public void BeginUnitTurn() //Rozpoczyna ture
    {
        unitTurn = true;
    }

    public void EndUnitTurn() //Konczy ture
    {
        unitTurn = false;
    }
}
