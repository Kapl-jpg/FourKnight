using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GenerationOfDungeon : MonoBehaviour
{
    private GameObject grid;
    [SerializeField] private GeneralInformation generalInformation;
    [SerializeField] private GameObject startLevel;
    private readonly List<GameObject> _poolRooms = new List<GameObject>();
    private readonly List<Transform> _beginTransform = new List<Transform>();
    private readonly List<Transform> _endTransform = new List<Transform>();
    [SerializeField] private Object[] ceilingTile;
    [SerializeField] private Object[] leftWallTile;
    private Tilemap _tilemapLeftWall;
    [SerializeField] private Object[] rightWallTile;
    private Tilemap _tilemapRightWall;
    [SerializeField] private Object[] floorTile;
    [SerializeField] private Object[] backgroundTile;
    [SerializeField] private Object[] gateTile;
    [SerializeField] private Object[] platformTile;
    [SerializeField] private Object[] stair;
    private List<int> _variableSpawnInts = new List<int>();
    [SerializeField] private int distanceBetweenPlatform;
    [SerializeField] private int minimumQuantityPlatform;
    [SerializeField] private int maximumQuantityPlatform;
    [SerializeField] private int minimumCountPlatform;
    [SerializeField] private int borderPlatform;
    [SerializeField] private int sizeGate;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private int distanceBetweenStairs = 1;
    private List<List<int>> _positionAndLenghtPlatforms = new List<List<int>>();
    private List<List<int>> previousValue = new List<List<int>>();
    
    private void Awake()
    {
        CreateStartLevel();
    }

    void Start()
    {
        CreateLevels();
    }

    private void CreateStartLevel()
    {
        var start = Instantiate(startLevel);
        _poolRooms.Add(start);
        _beginTransform.Add(start.GetComponent<SpecialRoom>().BeginMap);
        _endTransform.Add(start.GetComponent<SpecialRoom>().EndMap);
        start.GetComponent<SpecialRoom>().GeneralInformation = generalInformation;
        generalInformation.SpawnPointMainKnight = start.GetComponent<SpecialRoom>().SpawnKnight;
        start.GetComponent<SpecialRoom>().GenerationOfDungeon = this;
    }

    private void CreateLevels()
    {
        for (int i = 0; i < 1; i++)
        {
            grid = new GameObject("Level (" + i + ")");
            grid.AddComponent<Grid>();
            grid.GetComponent<Grid>().cellSize = new Vector3(.2f, .2f, 0);
            Floor(grid);
            Background(grid);
            LeftWall(grid);
            RightWall(grid);
            Ceiling(grid);
            //Gates(grid);
            Platform(grid);
           /* if (_positionAndLenghtPlatforms.Count > 0)
            {
                Stairs(grid);
            }*/
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(grid);
            CreateLevels();
        }
    }

    void Stairs(GameObject grid, List<List<int>> spawnInfo, int i)
    {
        /*
                GameObject tilemap = null;
                var _variableX = new List<int>();
                _positionAndLenghtPlatforms.Sort((i0, i1) => i0[1].CompareTo(i1[1]));
        
                for (int i = 0; i < _positionAndLenghtPlatforms.Count; i++)
                {
                    bool _create = true;
                    if (_create)
                    {
                        tilemap = new GameObject("Stairs (" + i + ")");
                        tilemap.AddComponent<Tilemap>();
                        tilemap.AddComponent<TilemapRenderer>();
                        tilemap.transform.SetParent(grid.transform);
                        tilemap.GetComponent<TilemapRenderer>().sortingOrder = 2;
                    }
        
                    List<int> spawnPrevious = new List<int>();
                    List<int> spawnCurrent = new List<int>();
                    List<int> result = new List<int>();
                    var lenghtStair = 0;
        
                    if (i > 0)
                    {
                        for (int j = _positionAndLenghtPlatforms[i - 1][0];
                            j < _positionAndLenghtPlatforms[i - 1][0] + _positionAndLenghtPlatforms[i - 1][2];
                            j++)
                        {
                            spawnPrevious.Add(j);
                        }
        
                        for (int j = _positionAndLenghtPlatforms[i][0];
                            j < _positionAndLenghtPlatforms[i][0] + _positionAndLenghtPlatforms[i][2];
                            j++)
                        {
                            spawnCurrent.Add(j);
                        }
        
                        foreach (var item in spawnPrevious.Where(item => spawnCurrent.Contains(item)))
                        {
                            result.Add(item);
                        }
                        foreach (var item in _variableX)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                result.Remove(item - 1 + k);
                            }
                        }
        
                        if (result.Count > 0)
                        {
                            lenghtStair = Mathf.Abs(_positionAndLenghtPlatforms[i - 1][1] - _positionAndLenghtPlatforms[i][1]);
                        }
                        else
                        {
                            for (int j = 0; j < i; j++)
                            {
                                spawnPrevious.Clear();
                                for (int k = _positionAndLenghtPlatforms[j][0];
                                    k < _positionAndLenghtPlatforms[j][0] + _positionAndLenghtPlatforms[j][2];
                                    k++)
                                {
                                    spawnPrevious.Add(k);
                                }
        
                                foreach (var item in spawnPrevious.Where(item => spawnCurrent.Contains(item)))
                                {
                                    result.Add(item);
                                }
        
                                if (result.Count > 0)
                                {
                                    lenghtStair =
                                        Mathf.Abs(_positionAndLenghtPlatforms[j][1] - _positionAndLenghtPlatforms[i][1]);
                                }
                                else
                                {
                                    print(1111111);
                                    lenghtStair = Mathf.Abs(_positionAndLenghtPlatforms[i][1]);
                                    result = spawnCurrent;
                                    foreach (var item in _variableX)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            result.Remove(item - 1 + k);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lenghtStair = _positionAndLenghtPlatforms[i][1];
                        for (int j = _positionAndLenghtPlatforms[i][0];
                            j < _positionAndLenghtPlatforms[i][0] + _positionAndLenghtPlatforms[i][2];
                            j++)
                        {
                            result.Add(j);
                            print(j);
                        }
                    }
        
                    var spawnY = _positionAndLenghtPlatforms[i][1];
                    var spawnX = Random.Range(0, result.Count - 1);
        
                    for (int k = 0; k < lenghtStair; k++)
                    {
                        if (result.Count > 0)
                        {
                            tilemap.GetComponent<Tilemap>()
                                .SetTile(new Vector3Int(result[spawnX], spawnY - k, 0), stair[0] as Tile);
                        }
                    }
        
                    _variableX.Add(result[spawnX]);
                }
        _positionAndLenghtPlatforms.Sort((i0, i1) => i0[1].CompareTo(i1[1]));
        List<List<int>> _previousValues = new List<List<int>>();
        for (int i = 0; i < _positionAndLenghtPlatforms.Count; i++)
        {
            
            var tilemap = new GameObject("Stairs (" + i + ")");
            tilemap.AddComponent<Tilemap>();
            tilemap.AddComponent<TilemapRenderer>();
            tilemap.transform.SetParent(grid.transform);
            tilemap.GetComponent<TilemapRenderer>().sortingOrder = 2;
            
            var startPoint = _positionAndLenghtPlatforms[i][0];
            var endPoint = _positionAndLenghtPlatforms[i][0] + _positionAndLenghtPlatforms[i][2];
            if (endPoint > width - 2)
            {
                endPoint = width - 2;
            }
            var lenghtStair = _positionAndLenghtPlatforms[i][1];
            var spawnX = 0;
            var spawnY = _positionAndLenghtPlatforms[i][1];
            List<int> variableSpawnX = new List<int>();
            for (int j = startPoint; j < endPoint; j++)
            {
                variableSpawnX.Add(j);
            }
            if (i > 0)
            {
                for (int j = 0; j < _previousValues.Count; j++)
                {
                    for (int k = 0; k < _previousValues[j].Count; k++)
                    {
                        spawnX = Random.Range(startPoint, endPoint);
                    }
                }
            }
            else
            {
                spawnX = Random.Range(startPoint, endPoint);
            }

            for (int j = 0; j < _previousValues.Count; j++)
            {
                foreach (var item in _previousValues[j].Where(item => variableSpawnX.Contains(item)))
                {
                    variableSpawnX.Remove(item);
                }
            }

            for (int j = 0; j < lenghtStair; j++)
            {
                tilemap.GetComponent<Tilemap>()
                    .SetTile(new Vector3Int(spawnX, spawnY - j, 0), stair[0] as Tile);
            }
            List<int> _intermediateValues = new List<int>();
            for (int j = 0; j < distanceBetweenStairs * 2 + 1; j++)
            {
                _intermediateValues.Add(spawnX - 1 + j);
            }
            _previousValues.Add(_intermediateValues);
        }*/

        var startPositionX = spawnInfo[i][0];
        var platformPositionY = spawnInfo[i][1];
        var endPositionX = spawnInfo[i][0] + spawnInfo[i][2];
        if (endPositionX > width + 1)
        {
            endPositionX = width + 1;
        }

        //**********************************************************
        //Создаётся объект и к нему добавляются разные компоненты и настраиваются
        GameObject tilemap = new GameObject("Stairs (" + i + ")"); //Создаётся сам объект
        tilemap.AddComponent<Tilemap>(); //Добавляется компонент Tilemap
        tilemap.AddComponent<TilemapRenderer>(); //Добавляется компонент TilemapRenderer
        tilemap.transform.SetParent(grid.transform); //Текущий объект становится дочерним объектом уровня
        tilemap.GetComponent<TilemapRenderer>().sortingOrder = 1; //Слой 1
        //**********************************************************
        int spawnX = 0;
        int lenghtStair = 0;

        if (i <= 0)
        {
            spawnX = Random.Range(startPositionX, endPositionX);
            lenghtStair = platformPositionY;
        } //Правила генерации первой ступени
        else
        {
            for (int k = 0; k < i; k++)
            {
                List<int> result = new List<int>();
                List<int> currentSpawnX = new List<int>();
                List<int> previousSpawnX = new List<int>();
                List<int> exceptions = new List<int>();
                try
                {
                    for (int j = startPositionX; j < endPositionX; j++)
                    {
                        currentSpawnX.Add(j);
                    }
                }
                catch
                {
                    print("Error 1");
                }

                /*try
                {
                    for (int j = 0; j < previousValue[i-1-k].Count; j++)
                    {
                        exceptions.Add(previousValue[i-1-k][j]);
                        print(previousValue[i-1-k][j] + " - i = " +i);
                    }
                }
                catch
                {
                    print("Error 2");
                }*/

                try
                {
                    var previousStartPosition = spawnInfo[i - 1 - k][0];
                    var previousEndPosition = spawnInfo[i - 1 - k][0] + spawnInfo[i - 1 - k][2];
                    if (previousEndPosition > width + 1)
                    {
                        previousEndPosition = width + 1;
                    }

                    for (int j = previousStartPosition; j < previousEndPosition; j++)
                    {
                        previousSpawnX.Add(j);
                    }
                }
                catch
                {
                    print("Error 3");
                }

                try
                {
                    foreach (var item in previousSpawnX.Where(item => currentSpawnX.Contains(item)))
                    {
                        result.Add(item);
                    }
                }
                catch
                {
                    print("Error 4");
                }

                try
                {
                    foreach (var item in previousValue[i-1-k])
                    {
                        result.Remove(item);
                    }

                    foreach (var item in result)
                    {
                        print(item + " = После удаления предыдущих исключений");
                    }

                }
                catch
                {
                    for (int j = 0; j < result.Count; j++)
                    {
                        // print(result[j]);
                    }

                    print("Error 5");
                }

                if (result.Count <= 0&&k<i-1)
                {
                    continue;
                }

                try
                {
                    if (result.Count > 0)
                    {
                        var randomValue = Random.Range(0, result.Count - 1);
                        spawnX = result[randomValue];
                        lenghtStair = Mathf.Abs(platformPositionY - spawnInfo[i - 1 - k][1]);
                        k = i;

                    }
                    else
                    {
                        for (int j = startPositionX; j < endPositionX; j++)
                        {
                            result.Add(j);
                        }

                        foreach (var item in previousValue[i-1-k])
                        {
                            result.Remove(item);
                        }
                        var randomValue = Random.Range(0, result.Count - 1);
                        spawnX = result[randomValue];
                        lenghtStair = platformPositionY ;
                    }
                }catch
                {
                    print("Error 6");
                
                }
            }

        } //Правила генерации всех остальных ступеней

        List<int> fallsPosition = new List<int>();
        for (int k = 0; k < distanceBetweenStairs * 2 + 1; k++)
        {
            fallsPosition.Add(spawnX - distanceBetweenStairs + k);
        }
        previousValue.Add(fallsPosition);
        
        for (int j = 0; j < previousValue[i].Count; j++)
        {
            print("Исключения = " +previousValue[i][j]+" i = " + i);
        }
        for (int j = 0; j < lenghtStair; j++)
        {
            tilemap.GetComponent<Tilemap>()
                .SetTile(new Vector3Int(spawnX, platformPositionY - j, 0), stair[0] as Tile);
        }
    }

    void Platform(GameObject grid)
    {
        //Просматриваются все варианты построения платформ по оси Y
        for (int i = borderPlatform; i < height - 3 - borderPlatform; i++)
        {
            _variableSpawnInts.Add(i);
        }

        //Выставляется рандомное количество платформ
        int randomQuantityPlatform = Random.Range(minimumQuantityPlatform, maximumQuantityPlatform);

        List<List<int>> spawnInfo = new List<List<int>>();
        //Цикл выполняется столько раз, сколько платформ
        for (int i = 0; i < randomQuantityPlatform; i++)
        {
            //Если вариантов больше нет, то заканчиваем цикл
            if (_variableSpawnInts.Count <= 0)
            {
                break;
            }

            var randomLenghtPlatform =
                Random.Range(minimumCountPlatform, width); //Выставляется рандомная длина платформы
            var randomPositionX = Random.Range(1, width); //Рандомная позиция по оси X
            var randomPositionY = Random.Range(0, _variableSpawnInts.Count - 1); //Рандомная позиция по оси Y
            var valuePositionX = randomPositionX / 2; //Высчитывается место начала спавна
            var positionX = randomPositionX - Mathf.RoundToInt(valuePositionX); //Выравнивается позиция по X
            var randomPosition =
                new Vector2Int(positionX, _variableSpawnInts[randomPositionY]); //Задаётся рандомная позиция
            //Удаляем из всех возможных вариантов те, которые уже добавлены
            for (int k = 0; k < distanceBetweenPlatform * 2 + 1; k++)
            {
                _variableSpawnInts.Remove(randomPosition.y + distanceBetweenPlatform - k);
            }

            List<int> rememberValues = new List<int>();
            rememberValues.Add(randomPosition.x);
            rememberValues.Add(randomPosition.y);
            rememberValues.Add(randomLenghtPlatform);
            spawnInfo.Add(rememberValues);
        }
        spawnInfo.Sort((i0, i1) => i0[1].CompareTo(i1[1]));
        for (int i = 0; i < spawnInfo.Count; i++)
        {
            //Создаётся объект и к нему добавляются разные компоненты и настраиваются
            GameObject tilemap = new GameObject("Platform (" + i + ")"); //Создаётся сам объект
            tilemap.AddComponent<Tilemap>(); //Добавляется компонент Tilemap
            tilemap.AddComponent<TilemapRenderer>(); //Добавляется компонент TilemapRenderer
            tilemap.transform.SetParent(grid.transform); //Текущий объект становится дочерним объектом уровня
            tilemap.GetComponent<TilemapRenderer>().sortingOrder = 1; //Слой 1

            int x = 0;
            int j = 0;
            var positionX = spawnInfo[i][0];
            var positionY = spawnInfo[i][1];
            var lenghtPlatforms = spawnInfo[i][2];
            
            while (x < lenghtPlatforms) //Цикл на рисование платформы
            {
                if (x == lenghtPlatforms - 1)
                {
                    j = platformTile.Length - 1;
                }

                if (positionX + x < width+1) //Рисование платформы
                {
                    tilemap.GetComponent<Tilemap>().SetTile(
                        new Vector3Int(positionX + x, positionY, 0), platformTile[j] as Tile);
                    j++;
                    x++;
                }
                else //Если нарисовали на всю длину, тогда заканчиваем цикл
                {
                    break;
                }

                if (x < lenghtPlatforms - 1) //Если закончились тайлы, то начинаем заново
                {
                    if (j >= platformTile.Length - 2)
                        j = 1;
                }
            }
            Stairs(grid,spawnInfo,i);
        }
    }

    private void Gates(GameObject grid)
    {
        GameObject tilemap = new GameObject("Gates");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(grid.transform);
        tilemap.GetComponent<TilemapRenderer>().sortingOrder = 1;

        int randomPositionX = Random.Range(0, width - sizeGate);
        int randomPositionY = Random.Range(0, height - sizeGate);
        int randomSide = Random.Range(0, 2);
        var tilemapComponent = tilemap.GetComponent<Tilemap>();
        switch (randomSide)
        {
            case 0:
                for (int i = 0; i < sizeGate; i++)
                {
                    tilemapComponent.SetTile(new Vector3Int(0, randomPositionY + i, 0), gateTile[0] as Tile);
                    _tilemapLeftWall.SetTile(new Vector3Int(0, randomPositionY + i, 0), null);
                }

                break;
            case 1:
                for (int i = 0; i < sizeGate; i++)
                {
                    tilemapComponent.SetTile(new Vector3Int(width - 2, randomPositionY + i, 0), gateTile[0] as Tile);
                    _tilemapRightWall.SetTile(new Vector3Int(width - 2, randomPositionY + i, 0), null);
                }

                break;
        }
    }

    private void Ceiling(GameObject grid)
    {
        GameObject tilemap = new GameObject("Ceiling");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(grid.transform);

        int x = 0;
        int j = 0;
        int ceilingCount = width ;
        while (x < ceilingCount)
        {
            tilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(1 + x, height+1, 0), ceilingTile[j] as Tile);
            j++;
            if (j == ceilingTile.Length - 1)
                j = 0;
            x++;
        }
    }

    private void LeftWall(GameObject grid)
    {
        GameObject tilemap = new GameObject("LeftWall");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(grid.transform);
        _tilemapLeftWall = tilemap.GetComponent<Tilemap>();

        int y = 0;
        int j = 0;
        int leftWallCount = height;
        while (y < leftWallCount)
        {
            tilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(0, 1 + y, 0), leftWallTile[j] as Tile);
            j++;
            if (j == leftWallTile.Length - 1)
                j = 0;
            y++;
        }
    }

    private void RightWall(GameObject grid)
    {
        GameObject tilemap = new GameObject("RightWall");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(grid.transform);
        _tilemapRightWall = tilemap.GetComponent<Tilemap>();
        int y = 0;
        int j = 0;
        int rightWallCount = height;
        while (y < rightWallCount)
        {
            tilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(width+1, 1 + y, 0), rightWallTile[j] as Tile);
            j++;
            if (j == rightWallTile.Length - 1)
                j = 0;
            y++;
        }
    }

    private int _backgroundLayer;

    private void Background(GameObject grid)
    {
        GameObject tilemap = new GameObject("Background");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(grid.transform);

        int backgroundHeight = height;
        int backgroundWidth = width;
        for (int x = 0; x < backgroundWidth; x++)
        {
            if (x % 3 == 0 && _backgroundLayer >= 2)
            {
                _backgroundLayer = 0;
            }
            else if (_backgroundLayer < 2)
            {
                _backgroundLayer++;
            }

            for (int y = 0; y < backgroundHeight; y++)
            {
                if (y % 2 == 0)
                    tilemap.GetComponent<Tilemap>()
                        .SetTile(new Vector3Int(1 + x, 1 + y, 0),
                            backgroundTile[_backgroundLayer + 3] as Tile);
                else
                    tilemap.GetComponent<Tilemap>()
                        .SetTile(new Vector3Int(1 + x, 1 + y, 0),
                            backgroundTile[_backgroundLayer] as Tile);
            }
        }
    }

    private void Floor(GameObject grid)
    {
        GameObject tilemap = new GameObject("Floor");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(grid.transform);

        int x = 0;
        int j = 0;
        int floorCount = width;
        while (x < floorCount)
        {
            tilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(1 + x, 0, 0), floorTile[j] as Tile);
            j++;
            if (j == floorTile.Length - 1)
                j = 0;
            x++;
            
        }
    }
}