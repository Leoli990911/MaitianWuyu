using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//Create an ExampleArmy
public class ExampleArmy : MonoBehaviour
{
    private FormationBase _formation;

    public FormationBase Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private  List<Vector2> _points = new List<Vector2>();

    private void Awake()
    {
        SetFormation();
    }
    private void Update()
    {
        SetFormation();
    }

    private void SetFormation()
    {
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count)
        {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count)
        {
            Debug.Log("Killinto: " + _spawnedUnits.Count);
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++)
        {
            _spawnedUnits[i].transform.position = Vector2.MoveTowards(
                (Vector2)_spawnedUnits[i].transform.position,
                (Vector2)(transform.position + new Vector3(_points[i].x, _points[i].y, 0)),
                _unitSpeed * Time.deltaTime
            );
        }
    }

    private void Spawn(IEnumerable<Vector2> points)
    {
        foreach (var pos in points)
        {
            var unit = Instantiate(_unitPrefab, (Vector2)transform.position + pos, Quaternion.identity,transform);
            _spawnedUnits.Add(unit);
        }
    }

    private void Kill(int num)
    {
        for (var i = 0; i < num; i++)
        {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
}