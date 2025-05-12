using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrawlsTars
{
    public abstract class BrawlerAbility
    {
        public string Name { get; }

        public abstract void Use();
    }

    public class PiercingStrike : BrawlerAbility
    {
        public string Name { get; } = "Piercing Strike";
        private string UserName { get; set; }

        public PiercingStrike(string userName)
        {
            UserName = userName;
        }
        public override void Use()
        {
            Console.WriteLine($"{UserName} is Used {Name}");
        }
    }
    
    

}
