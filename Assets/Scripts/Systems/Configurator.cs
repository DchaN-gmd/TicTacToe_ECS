using UnityEngine;

[CreateAssetMenu(fileName = "New Configurator", menuName = "Data/Configurator", order = 51)]
public class Configurator : ScriptableObject, IConfigurator
{
    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private GameObject _crossPrefab;
    [SerializeField] private GameObject _circlePrefab;

    [SerializeField] private int _borderHeight = 3;
    [SerializeField] private int _borderWidht = 3;
    [SerializeField] [Min(0)] private int _countToWin = 3;

    public GameObject CellPrefab => _cellPrefab;
    public GameObject CrossPrefab => _crossPrefab;
    public GameObject CirclePrefab => _circlePrefab;
    public int BorderHeight => _borderHeight;
    public int BorderWidth => _borderWidht;
    public int CountToWin => _countToWin;

}