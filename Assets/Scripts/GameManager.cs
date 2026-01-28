using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _tray;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var vector3 = _tray.transform.position;
            vector3.x = 0;
            _tray.transform.position = vector3;

            List<Ball> balls =
                new List<Ball>(FindObjectsByType<Ball>(FindObjectsInactive.Exclude, FindObjectsSortMode.None));
            foreach (var ball in balls)
            {
                Destroy(ball.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}