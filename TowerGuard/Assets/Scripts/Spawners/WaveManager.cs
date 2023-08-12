using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    private void Start()
    {
        StartWave(0);
    }

    private void StartWave(int waveIndex)
    {
        _currentWaveIndex = waveIndex;
        _currentEnemyIndex = 0;
        _enemiesLeftToSpawn = _waves[waveIndex].WaveSettings.Length;

        // ��������� ��� �������� ����� ������� �����
        foreach (var waveSetting in _waves[waveIndex].WaveSettings)
        {
            waveSetting.DeactivateSpawner();
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_currentEnemyIndex < _waves[_currentWaveIndex].WaveSettings.Length)
        {
            WaveSettings waveSettings = _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex];

            waveSettings.ActivateSpawner(); // ���������� �������

            yield return new WaitForSeconds(waveSettings.SpawnDelay);

            waveSettings.DeactivateSpawner(); // ��������� �������

            _currentEnemyIndex++;
        }

        _currentEnemyIndex = 0; // ����� �������� ��� ��������� �����
        _enemiesLeftToSpawn = 0; // ����� �������� ��� ��������� �����

        _currentWaveIndex++;
        if (_currentWaveIndex < _waves.Length)
        {
            StartWave(_currentWaveIndex); // ��������� ��������� �����
        }
        else
        {
            // ��� ����� ���������, ��������� �������
        }
    }
}

[System.Serializable]
public class Waves
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _spawner;
    public GameObject Spawner { get => _spawner; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }

    public void ActivateSpawner()
    {
        Spawner.SetActive(true);
    }

    public void DeactivateSpawner()
    {
        Spawner.SetActive(false);
    }
}