using Cinemachine;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Tamagochi tamagochi;
    [SerializeField] private TamagochiGUI tamagochiGUI;
    [SerializeField] private GameController gameController;
    [SerializeField] private PositionController positionController;
    [SerializeField] private DataPersistenceManager dataPersistenceManager;

    private void Awake()
    {
        tamagochiGUI.Initialize(tamagochi);

        var bodies = tamagochi.GetComponentsInChildren<TamagochiFeed>(true).Select(c => c.gameObject).ToArray();
        tamagochi.Initialize(bodies);

        dataPersistenceManager.Initialize();

        positionController.Initialize(tamagochi);

        var lightsSources = GameObject.FindGameObjectsWithTag("MainLight");
        gameController.Initialize(lightsSources, positionController, tamagochi);
    }
}
