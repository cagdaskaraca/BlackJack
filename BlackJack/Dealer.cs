using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CardGames
{
    [DataContract]
    class Dealer : CardPlayer
    {
        private ConsoleColor hidden;
        public Dealer(string name) : base(name)
        {

        }

        public ConsoleColor Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }
    }
}
