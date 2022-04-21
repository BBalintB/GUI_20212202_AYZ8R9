using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Models.NPC;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class FightWindowLogic : IFightWindowLogic
    {
        IList<Models.ICharacter> heroes;
        IList<Models.ICharacter> availableHeroes;
        IList<Models.ICharacter> villians;
        IList<string> log;
        IMessenger messenger;
        int round;
        int roundCounter;
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

        public void SetupCollections(IList<Models.ICharacter> heroes, IList<Models.ICharacter> villians, IList<Models.ICharacter> availableHeroes, IList<string> log)
        {
            this.heroes = heroes;
            this.villians = villians;
            this.availableHeroes = availableHeroes;
            this.log = log;
            round = 1;
            roundCounter = 0;
            availableHeroes.Add(new NPC_Character()
            {
                Name = "Pista",
                HP = 100,
                Attack = 35,
                SpecialAttackCounter = 0,
                HeroType = HeroTypes.Assault
            });

            availableHeroes.Add(new NPC_Character()
            {
                Name = "Jóska",
                HP = 100,
                Attack = 20,
                SpecialAttackCounter = 0,
                HeroType = HeroTypes.Support
            });

            availableHeroes.Add(new NPC_Character()
            {
                Name = "Kati",
                HP = 100,
                Attack = 45,
                SpecialAttackCounter = 0,
                HeroType = HeroTypes.Archer
            });

            availableHeroes.Add(new NPC_Character()
            {
                Name = "Killmogger",
                HP = 100,
                Attack = 30,
                SpecialAttackCounter = 0,
                HeroType = HeroTypes.Assault
            });

            availableHeroes.Add(new NPC_Character()
            {
                Name = "Supi",
                HP = 100,
                Attack = 15,
                SpecialAttackCounter = 0,
                HeroType = HeroTypes.Support
            });


            #region tmpenemys
            villians.Add(new NPC_Character()
            {
                Name = "bandit1",
                HP = 100,
                Attack = 20,
                HeroType = HeroTypes.Bandit
            });
            villians.Add(new NPC_Character()
            {
                Name = "bandit2",
                HP = 100,
                Attack = 20,
                HeroType = HeroTypes.Bandit
            });
            villians.Add(new NPC_Character()
            {
                Name = "bandit3",
                HP = 100,
                Attack = 20,
                HeroType = HeroTypes.Bandit
            });
            #endregion

            messenger.Send("Round changed", "RoundInfo");
        }

        #region SelectorLogic

        public void AddToTeam(Models.ICharacter hero) {
            if (heroes.Count<5)
            {
                heroes.Add(hero);
                availableHeroes.Remove(hero);
            }
            
        }

        public void RemoveFromTeam(Models.ICharacter hero) {
            if (hero is not Hero)
            {
                availableHeroes.Add(hero);
                heroes.Remove(hero);
            }
        }

        int clicked = 0;

        public void GetBooster() {

            if (clicked == 0 )
            {
                foreach (var hero in heroes)
                {
                    hero.HP += 15;
                    hero.Attack += 10;
                }
                clicked++;
            }
            

        }

        #endregion

        #region FightLogic
        public void Attack(Models.ICharacter from, Models.ICharacter to)
        {
            from.Position = RoundPosition.Attack;
            to.HP -= from.Attack;
            from.SpecialAttackCounter++;
            log.Add($"- Round: {round}\n- {from.Name} attacked {to.Name} with {from.Attack} dmg");
            if (to.HP<=0)
            {
                if (heroes.Contains(to))
                {
                    heroes.Remove(to);
                }
                else {
                    villians.Remove(to);
                }
                log.Add($"- Round: {round}\n- {from.Name} killed {to.Name}");
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
            log.Add($"- Round: {round}\n- {character.Name} healed himself 25 hp");
            messenger.Send("Heal happened", "RoundInfo");
        }

        public void Special(Models.ICharacter character, Models.ICharacter enemy)
        {
            log.Add($"- Round: {round}\n- {character.Name} special power activated");
            character.Position = RoundPosition.Special;
            character.SpecialAttackCounter = 0;
            roundCounter++;
            switch (character.HeroType)
            {
                case HeroTypes.Neutral:
                    break;
                case HeroTypes.Archer:
                    for (int i = 0; i < 2; i++)
                    {
                        Attack(character, enemy);
                    }
                    break;
                case HeroTypes.Assault:
                    character.Attack *= 2;
                    this.Attack(character, enemy);
                    character.Attack /= 2;
                    break;
                case HeroTypes.Support:
                    
                    break;
                case HeroTypes.Medic:
                    if (heroes.Contains(character))
                    {
                        foreach (var item in heroes)
                        {
                            item.HP += 20;
                        }
                    }
                    else {
                        foreach (var item in villians)
                        {
                            item.HP += 20;
                        }
                    }
                    break;
                case HeroTypes.Heavy:
                    character.HP += 100;
                    break;
                case HeroTypes.Sniper:

                    break;
                case HeroTypes.Specialist:
                    Attack(character, enemy);
                    if (RandomUtil.rnd.Next(0,101)<30)
                    {
                        var tmp1 = heroes.Contains(character) ? villians.OrderBy(x => x.HP).First() : heroes.OrderBy(x => x.HP).First();
                        Attack(character, tmp1);
                    }
                    break;
                case HeroTypes.Bandit:
                    if (heroes.Count != 0)
                    {
                        for (int i = 0; i < heroes.Count; i++)
                        {
                            Attack(character, heroes.ElementAt(i));
                        }
                        
                    }
                    break;
                case HeroTypes.Soldier:
                    var tmp2 = heroes.Contains(character) ? villians.OrderBy(x => x.HP).First() : heroes.OrderBy(x => x.HP).First();
                    tmp2.HP += 10;
                    break;
                case HeroTypes.Robot:
                    enemy.SpecialAttackCounter = -3;
                    break;
                case HeroTypes.Mercenary:

                    break;
            }
            log.Add($"- Round: {round}\n- {character.Name} special power deactivated");
            character.Position = RoundPosition.Special;
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
