using BrawlTars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrawlsTars
{
    public interface IBrawler
    {
        Player Player { get; }

        string Name {  get; }

        int Strength { get; }
        int Agility { get; }
        int Inteligence { get; }

        int Level { get; }
        int Maxlevel { get; }

        decimal MinXp { get; }
        decimal CurrentXp { get; }
        decimal MaxXp { get; }

        int CurrentEx { get; }
        int ExToNextLevel {  get; }

        BrawlerInventory Inventory { get; }
        BrawlerAbility BrawlerAbility { get; }

        HeroType HeroType { get; }
    }
    public class Warrior : IBrawler
    {
        public Player Player { get; private set; }

        public string Name { get; private set; } = "Warrior";

        public int Strength { get; private set; } = 20;
        public int Agility { get; private set; } = 10;
        public int Inteligence { get; private set; } = 7;

        public int Level { get; private set; }
        public int Maxlevel { get; private set; } = 30;

        public decimal MinXp { get; private set; } = 0;
        public decimal CurrentXp { get; private set; } = 1000m;
        public decimal MaxXp { get; private set; } = 1000m;

        public int CurrentEx { get; private set; } = 0;
        public int ExToNextLevel { get; private set; } = 100;

        public BrawlerInventory Inventory { get; private set; } 
        public BrawlerAbility BrawlerAbility { get; private set; }

        public HeroType HeroType { get; private set; } = HeroType.Warrior;


        private Warrior(Player player)
        {
            Player = player;
            Inventory = new BrawlerInventory();
            BrawlerAbility = new PiercingStrike(Player.Name);
        }

        public static Warrior Choose(Player player)
        {
            return new Warrior(player);
        }

        public void UseAbility1()
        {
            BrawlerAbility.Use();
        }
        
    }

}
