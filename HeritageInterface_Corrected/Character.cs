using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HeritageEtInterfaceCorrection
{
    class Character
    {
        public string Name { get; set; }

        protected int Attack { get; set; }
        protected int Defense { get; set; }
        protected int Initiative { get; set; }
        protected int Damages { get; set; }
        protected int MaxLife { get; set; }

        protected int CurrentCounterBonus { get; set; }
        public int CurrentLife { get; set; }
        public int CurrentInitiative { get; set; }
        public int CurrentAttackNumber { get; set; }

        public bool CanAttack { get; set; }
        public int MaxAttackNumber { get; set; }

        [JsonIgnore]
        public FightManager fightManager;

        public ConsoleColor Color { get; set; }

        protected Random random;
        public int RandomSeed { get; set; }
        protected bool HolyDamages { get; set; }
        protected bool UnholyDamages { get; set; }
        protected bool Blessed { get; set; }
        protected bool Cursed { get; set; }

        public Character(string name, int attack, int defense, int initiative, int damages, int maxLife, int maxAttackNumber = 1, bool holyDamages = false, bool unholyDamages = false, bool blessed = false, bool cursed = false, ConsoleColor color = ConsoleColor.White)
        {
            Name = name;
            this.Attack = attack;
            Defense = defense;
            Initiative = initiative;
            Damages = damages;
            MaxLife = maxLife;
            MaxAttackNumber = maxAttackNumber;
            HolyDamages = holyDamages;
            UnholyDamages = unholyDamages;
            Cursed = cursed;
            Blessed = blessed;
            Color = color;
            RandomSeed = NameToInt() + (int)DateTime.Now.Ticks;
            this.random = new Random(RandomSeed);
            Reset();
        }

        public void SetFightManager(FightManager fightManager)
        {
            this.fightManager = fightManager;
        }

        public virtual void Reset()
        {
            CurrentLife = MaxLife;
            CanAttack = true;
            CurrentAttackNumber = MaxAttackNumber;
        }

        public virtual void RoundReset()
        {
            //reset du bonus de contre attaque
            CurrentCounterBonus = 0;
            CurrentAttackNumber = MaxAttackNumber;
            CanAttackReset();
        }

        public virtual void CanAttackReset()
        {
            CanAttack = true;
        }

        protected int RoundToInt(float value)
        {
            return (int)Math.Round((double)value);
        }

        public virtual int RollDice()
        {
            return random.Next(1, 101);
        }

        public int NameToInt()
        {
            int result = 0;
            foreach (char c in Name)
            {
                result += c;
            }
            return result;
        }

        public void CalculateInitiative()
        {
            CurrentInitiative = Initiative + RollDice();
            MyLog(Name + " initiative " + CurrentInitiative);
        }

        protected void MakeAnAttack(Character target)
        {
            CurrentAttackNumber--;
            MyLog(Name + " attaque " + target.Name + ".");
            target.Defend(Attack + RollDice(), Damages, this, true);
        }

        public virtual void Defend(int _attackValue, int _damage, Character _attacker, bool canBeCountered)
        {
            //On calcule la marge d'attaque
            //en soustrayant le jet de defense du personnage qui defend au jet d'attaque reçu
            int AttaqueMargin = _attackValue - (Defense + RollDice());
            //Si la marge d'attaque est supérieure à 0
            if (AttaqueMargin > 0)
            {
                MyLog(Name + " se defend mais encaisse quand même le coup.");
                //on calcule les dégâts finaux
                int finalDamages = (int)(AttaqueMargin * _damage / 100f);
                if ((_attacker.UnholyDamages && Blessed) || (_attacker.HolyDamages && Cursed))
                {
                    TakeDamages(finalDamages*2);
                }
                else
                {
                    TakeDamages(finalDamages);
                }
            }
            else
            {
                //annoncer dans la console que le personnage a reussi sa defense
                MyLog(Name + " réussi sa défense.");
                if (_attacker != null && CanAttack && canBeCountered && CurrentAttackNumber > 0)
                {
                    Counter(-AttaqueMargin, _attacker);
                }
            }
        }

        public virtual void Counter(int _CounterBonus, Character Attacker)
        {
            CurrentAttackNumber--;
            //annoncer dans la console que le personnage contre-attaque
            MyLog(Name + " contre-attaque sur " + Attacker.Name + ".");
            //le personnage fait un jet d'Attaque. Le résultat est envoyé à l'adversaire
            Attacker.Defend(Attack + RollDice() + _CounterBonus, Damages, this, true);
        }

        public virtual void TakeDamages(int _damages)
        {
            MyLog(Name + " subis " + _damages + " points de dégats.");
            CurrentLife -= _damages;
            if (CurrentLife <= 0)
            {
                MyLog(Name + " est mort.");
            }
        }

        //selectionner une cible valide
        public virtual void SelectTargetAndAttack()
        {
            //on cree une liste dans laquelle on stockera les cibles valides
            List<Character> validTarget = new List<Character>();

            for (int i = 0; i < fightManager.charactersList.Count; i++)
            {
                Character currentCharacter = fightManager.charactersList[i];
                //si le personnage testé n'est pas celui qui attaque et qu'il est vivant
                if (currentCharacter != this && currentCharacter.CurrentLife > 0)
                {
                    //on l'ajoute à la liste des cible valide
                    validTarget.Add(currentCharacter);
                }
            }

            if (validTarget.Count > 0)
            {
                //on prend un personngae au hasard dans la liste des cibles valides et on le designe comme la cible de l'attaque 
                Character target = validTarget[random.Next(0, validTarget.Count)];
                MakeAnAttack(target);
            }
            else
            {
                MyLog(Name + " n'a pas trouvé de cible valide");
                CurrentAttackNumber = 0;
            }
        }

        public void MyLog(string text)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
