using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dievinity.Entities {
    public class Stats : IComparable<Stats> {

        public int Initiative { get; set; }

        public int MaxActionPoints { get; set; }

        private int actionPoints;
        public int ActionPoints {
            get { return actionPoints; }
            set {
                actionPoints = value;
                if (actionPoints > MaxActionPoints) {
                    actionPoints = MaxActionPoints;
                }
            }
        }

        public Stats() {
            Initiative = 1;
            MaxActionPoints = 4;
            ActionPoints = 4;
        }

        public int CompareTo(Stats other) {
            return other.Initiative.CompareTo(Initiative);
        }
    }
}
