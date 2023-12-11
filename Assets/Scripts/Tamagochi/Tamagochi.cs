using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using static TamagochiConstants;


public class Tamagochi : MonoBehaviour, IDataPersistence
{
    public event Action StatsChanged;

    [SerializeField] private GameObject[] bodies;

    [HideInInspector] public GameObject CurrBody;

    public float Satiety { get; private set; }
    public float Hygiene { get; private set; }
    public float Programming { get; private set; }
    public float Cheerfulness { get; private set; }

    [HideInInspector] public bool isSleeping = false;
    [HideInInspector] public bool IsDie = false;

    private DateTime last_levelup_time;

    private AgeState state;
    public AgeState State
    {
        get => state;
        private set
        {
            foreach (var body in bodies)
            {
                body.SetActive(false);
            }
            bodies[Convert.ToInt32(value)].transform.position = CurrBody.transform.position;
            CurrBody = bodies[Convert.ToInt32(value)];
            CurrBody.SetActive(true);
            state = value;
        }
    }
    private int level;

    public int Level
    {
        get => level;
        private set
        {
            if (value >= ADULT_LEVEL_CUP)
                State = AgeState.Adult;
            else if (value >= TEENAGER_LEVEL_CUP)
                State = AgeState.Teenager;
            else
                State = AgeState.Child;

            level = value;
        }
    }

    public void Initialize(GameObject[] _bodies)
    {
        bodies = _bodies;
    }

    public void Start()
    {
        InvokeRepeating(nameof(LifeCycle), 0, 60f);
    }

    public void LEVELTESTUP() {
        Level += 1;
        StatsChanged?.Invoke();
    }

    public void LEVELTESTDOWN() {
        Level -= 1;
        StatsChanged?.Invoke();
    }

    private void LifeCycle()
    {
        Satiety -= SATIETY_DIMINISH_PER_MINUTE;
        Hygiene -= HYGIENE_DIMINISH_PER_MINUTE;
        Programming -= PROGRAMMING_DIMINISH_PER_MINUTE;
        if (!isSleeping)
            Cheerfulness -= CHEERFULNESS_DIMINISH_PER_MINUTE;
        else
            Cheerfulness += CHEERFULNESS_ADD_PER_MINUTE;

        if (DateTime.UtcNow - last_levelup_time >= TIME_TO_LEVELUP)
        {
            last_levelup_time = DateTime.UtcNow;
            Level++;
        }

        if (Satiety <= 0 || Hygiene <= 0 || Programming <= 0 || Cheerfulness <= 0)
        {
            IsDie = true;
        }

        StatsChanged?.Invoke();
    }

    public void Feed()
    {
        if (!isSleeping)
        {
            Satiety += SATIETY_ADD;
            Satiety = Satiety > 100 ? 100 : Satiety;
            StatsChanged?.Invoke();
        }
    }

    public void Program()
    {
        if (!isSleeping)
        {
            Programming += PROGRAMMING_ADD;
            Programming = Programming > 100 ? 100 : Programming;
            StatsChanged?.Invoke();
        }
    }

    public void Wash()
    {
        if (!isSleeping)
        {
            Hygiene += HYGIENE_ADD;
            Hygiene = Hygiene > 100 ? 100 : Hygiene;
            StatsChanged?.Invoke();
        }
    }

    public bool ToggleSleep()
    {
        isSleeping = !isSleeping;
        CurrBody.GetComponent<MeshRenderer>().enabled = !isSleeping;
        return isSleeping;
    }

    public void LoadData(GameData data)
    {
        int minutes_away = (int)(DateTime.UtcNow - DateTime.FromFileTimeUtc(data.QuitTime)).TotalMinutes;

        this.Programming = data.Programming - minutes_away * PROGRAMMING_DIMINISH_PER_MINUTE;
        this.Satiety = data.Satiety - minutes_away * SATIETY_DIMINISH_PER_MINUTE;
        this.Hygiene = data.Hygiene - minutes_away * HYGIENE_DIMINISH_PER_MINUTE;
        this.last_levelup_time = DateTime.FromFileTimeUtc(data.LastLevelUpTime);

        CurrBody = this.bodies[Convert.ToInt32(data.State)];

        this.isSleeping = data.IsSleeping;
        this.Level = data.Level + (int)(DateTime.UtcNow - last_levelup_time).TotalDays;

        if (isSleeping)
        {
            this.Cheerfulness = data.Cheerfulness + minutes_away * CHEERFULNESS_ADD_PER_MINUTE;
            this.Cheerfulness = this.Cheerfulness > 100 ? 100 : this.Cheerfulness;

            CurrBody.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.Cheerfulness = data.Cheerfulness - minutes_away * CHEERFULNESS_DIMINISH_PER_MINUTE;
        }

        if (Satiety <= 0 || Hygiene <= 0 || Programming <= 0 || Cheerfulness <= 0)
        {
            IsDie = true;
        }
        else
        {
            IsDie = false;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.Level = this.level;
        data.QuitTime = DateTime.UtcNow.ToFileTimeUtc();
        data.Cheerfulness = this.Cheerfulness;
        data.Programming = this.Programming;
        data.Hygiene = this.Hygiene;
        data.Satiety = this.Satiety;
        data.LastLevelUpTime = this.last_levelup_time.ToFileTimeUtc();
        data.IsSleeping = this.isSleeping;
        data.State = this.State;
    }
}
