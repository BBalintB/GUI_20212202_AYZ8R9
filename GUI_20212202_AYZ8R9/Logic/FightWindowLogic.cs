using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class FightWindowLogic : IFightWindowLogic
    {
        IList<Models.ICharacter> heroes;
        IList<Models.ICharacter> availableHeroes;
        IList<Models.ICharacter> villians;
        IMessenger messenger;
        int round;
        int roundCounter;
        public bool Clicked { get; set; }
        public int Rounds
        {
            get
            {
                ;
                if (roundCounter >= (heroes.Count + villians.Count))
                {
                    foreach (var item in heroes)
                    {
                        item.Position = RoundPosition.Neutral;
                    }
                    roundCounter = 0;
                    return ++round;
                }
                else{
                    return round;
                }
            }
        }

        public FightWindowLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void SetupCollections(IList<Models.ICharacter> heroes, IList<Models.ICharacter> villians, IList<Models.ICharacter> availableHeroes)
        {
            this.heroes = heroes;
            this.villians = villians;
            this.availableHeroes = availableHeroes;
            round = 1;
            roundCounter = 0;
        }

        #region SelectorLogic

        public void AddToTeam(Models.ICharacter hero) {
            heroes.Add(hero);
            availableHeroes.Remove(hero);
        }

        public void RemoveFromTeam(Models.ICharacter hero) {
            if (hero is not Hero)
            {
                availableHeroes.Add(hero);
                heroes.Remove(hero);
            }
        }

        public void GetBooster() {
            if (RandomUtil.rnd.Next(0,101)<=10)
            {
                foreach (var hero in heroes)
                {
                    hero.HP += 15;
                    hero.Attack += 10;
                }
            }
            Clicked = true;
            messenger.Send("Click happened", "ClickInfo");
        }

        #endregion

        #region FightLogic
        public void Attack(Models.ICharacter from, Models.ICharacter to)
        {
            from.Position = RoundPosition.Attack;
            to.HP -= from.Attack;
            from.SpecialAttackCounter++;
            if (to.HP<=0)
            {
                if (heroes.Contains(to))
                {
                    heroes.Remove(to);
                }
                else {
                    villians.Remove(to);
                }
            }
            roundCounter++;
            messenger.Send("Attack happened", "RoundInfo");
        }

        public void Heal(Models.ICharacter character)
        {
            character.HP += 25;
            character.Position = RoundPosition.Heal;
            character.SpecialAttackCounter++;
            roundCounter++;
            messenger.Send("Heal happened", "RoundInfo");
        }

        public void Special(Models.ICharacter character, Models.ICharacter enemy)
        {
            character.Position = RoundPosition.Special;
            character.SpecialAttackCounter = 0;
            roundCounter++;
            switch (character.HeroType)
            {
                case HeroTypes.Neutral:
                    break;
                case HeroTypes.Archer:
                    break;
                case HeroTypes.Assault:
                    character.Attack *= 2;
                    this.Attack(character, enemy);
                    character.Attack /= 2;
                    break;
                case HeroTypes.Support:
                    break;
                case HeroTypes.Medic:
                    break;
                case HeroTypes.Heavy:
                    break;
                case HeroTypes.Sniper:
                    break;
                case HeroTypes.Specialist:
                    break;
                case HeroTypes.Bandit:
                    foreach (var item in heroes)
                    {
                        item.HP -= 10;
                    }
                    break;
                case HeroTypes.Soldier:
                    break;
                case HeroTypes.Robot:
                    break;
                case HeroTypes.Mercenary:
                    break;
            }
            
            //TODO special move
        }

        public void BotRound(Models.ICharacter from) {
            if (from.HP <= 50)
            {
                Heal(from);
            }
            else {
                var hero = heroes.OrderBy(x => x.HP).First();
                if (1 < from.SpecialAttackCounter)
                {
                    Special(from,hero);
                }
                else
                {
                    Attack(from, hero);
                }

            }
        }
        #endregion
    }
}
