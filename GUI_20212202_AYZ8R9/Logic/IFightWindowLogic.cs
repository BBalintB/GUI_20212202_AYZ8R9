﻿using GUI_20212202_AYZ8R9.Models;
using System.Collections.Generic;

namespace GUI_20212202_AYZ8R9.Logic
{
    public interface IFightWindowLogic
    {
        int Rounds { get; }

        void Attack(Models.ICharacter from, Models.ICharacter to);
        void Heal(Models.ICharacter character);
        void SetupCollections(IList<Models.ICharacter> heroes, IList<Models.ICharacter> villians, IList<Models.ICharacter> availableHeroes, IList<string> log, Game game);
        void Special(Models.ICharacter character, Models.ICharacter enemy);
        void BotRound(Models.ICharacter from);

        void AddToTeam(Models.ICharacter hero);
        void RemoveFromTeam(Models.ICharacter hero);
        void GetBooster();
    }
}