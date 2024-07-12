using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private List<Vector3> _positions;

    [SerializeField] private InputField _field;
    private float _time;

    private Vector3 _startPos;

    private float _velocity;

    private void Start()
    {
        _positions = new List<Vector3>();
        _startPos = this.transform.position;
        _field.text = "1";
    }

    private void Update()
    {
        float.TryParse(_field.text, out float res);
        _time = res;
        if(_field.text == null)
        {
            _time = 1;
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Vector3 hitPos = new Vector3(hit.point.x, 0, hit.point.z);
                _positions.Add(hitPos);
            }
        }

        if(_positions.Count == 1)
        {
            _velocity = Velocity(_startPos, _positions[0], _time);
        }

        if(_positions.Count > 0)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _positions[0], _velocity * Time.deltaTime);

            if(this.transform.position == _positions[0])
            {
                _positions.RemoveAt(0);

                _startPos = this.transform.position;
                if(_positions.Count != 0)
                {
                    _velocity = Velocity(_startPos, _positions[0], _time);
                }
                
            }
        }
    }

    private float Velocity(Vector3 startPos, Vector3 targetPos, float time)
    {
        return Vector3.Distance(startPos, targetPos) / time;
    }
}
