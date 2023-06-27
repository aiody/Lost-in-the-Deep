using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class CharacterFactory
    {
        public Character MakeCharacter(int characterNumber)
        {
            if (characterNumber == 1)
                return new Diver();
            else if (characterNumber == 2)
                return new MarineBiologist();
            else if (characterNumber == 3)
                return new Archaeologist();
            else
                return null;
        }
    }
}
