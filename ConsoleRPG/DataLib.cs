﻿using System;
using System.IO;
using System.Collections.Generic;

/*  Datalib class, reads and stores all the data from the input text files stored in the Data folder.
 *  If a class is expanded, the Read... function probably also needs some work */

namespace ConsoleRPG
{
    public class DataLib
    { 
        public List<Weapon> playerWeaponList;
        public List<Class> classList;
        public List<Skill> skillList;
        public List<Encounter> encounterList;
        public List<string> levelUpMessages;
        public List<Race> raceList;

        string dataFolder = @"../../../Data/";

        public DataLib()
        {
            ReadLevelUpLines();
            ReadRaces();
            ReadSkills();
            ReadClasses();
            ReadWeapons();
            ReadEncounters();
            Program.ut.TypeLine("Done loading, press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        void ReadLevelUpLines()
        {
            Program.ut.TypeLine("Loading text");
            string[] levelUpLines = System.IO.File.ReadAllLines(Path.Combine(dataFolder, "LevelUpLines.txt"));
            levelUpMessages = new List<string>();
            foreach (string s in levelUpLines)
            {
                if (s[0] != '/')
                {
                    levelUpMessages.Add(s);
                }
            }
        }

        void ReadRaces()
        {
            Program.ut.TypeLine("Loading races");
            string[] raceLines = System.IO.File.ReadAllLines(Path.Combine(dataFolder, "Races.txt"));
            raceList = new List<Race>();
            foreach (string s in raceLines)
            {
                if (s[0] != '/')
                {
                    string[] raceParts = s.Split(',');
                    string rName = raceParts[0];
                    int rStr = Int32.Parse(raceParts[1]);
                    int rAgi = Int32.Parse(raceParts[2]);
                    int rCon = Int32.Parse(raceParts[3]);
                    int rInt = Int32.Parse(raceParts[4]);
                    raceList.Add(new Race(rName,rStr,rAgi,rCon,rInt));
                }
            }
        }

        void ReadWeapons()
        {
            Program.ut.TypeLine("Loading weapons");
            string[] weaponLines = System.IO.File.ReadAllLines(Path.Combine(dataFolder, "Weapons.txt"));
            playerWeaponList = new List<Weapon>();
            foreach (string s in weaponLines)
            {
                if (s[0] != '/')
                {
                    string[] weaponParts = s.Split(',');
                    string wName = weaponParts[0];
                    int wMinDmg = Int32.Parse(weaponParts[1]);
                    int wMaxDmg = Int32.Parse(weaponParts[2]);
                    int wCrtCh = Int32.Parse(weaponParts[3]);
                    int wCrtMlt = Int32.Parse(weaponParts[4]);
                    playerWeaponList.Add(new Weapon(wName, wMinDmg, wMaxDmg, wCrtCh, wCrtMlt));
                }
            }
        }

        void ReadSkills()
        {
            Program.ut.TypeLine("Loading skills");
            string[] skillLines = System.IO.File.ReadAllLines(Path.Combine(dataFolder, "Skills.txt"));
            skillList = new List<Skill>();
            foreach (string s in skillLines)
            {
                if (s[0] != '/')
                {
                    string[] skillParts = s.Split(',');
                    string skillName = skillParts[0];
                    string skillDesc = skillParts[1];
                    string skillUse = skillParts[2];
                    int skillCD = Int32.Parse(skillParts[3]);
                    int skillDmgMod = Int32.Parse(skillParts[4]);
                    int skillClass = Int32.Parse(skillParts[5]);
                    bool skillAll = bool.Parse(skillParts[6]);
                    bool skillSelf = bool.Parse(skillParts[7]);
                    string sType = skillParts[8];
                    skillList.Add(new Skill(skillName, skillDesc, skillUse, skillCD, skillDmgMod, skillClass, skillAll, skillSelf, sType));
                }
            }
        }

        void ReadClasses()
        {
            Program.ut.TypeLine("Loading classes");
            string[] classLines = System.IO.File.ReadAllLines(Path.Combine(dataFolder, "Classes.txt"));
            classList = new List<Class>();
            foreach (string s in classLines)
            {
                if (s[0] != '/')
                {
                    string[] classParts = s.Split(',');
                    string className = classParts[0];
                    int classBHP = Int32.Parse(classParts[1]);
                    int classHPL = Int32.Parse(classParts[2]);
                    int classSMod = Int32.Parse(classParts[3]);
                    int classAMod = Int32.Parse(classParts[4]);
                    int classCMod = Int32.Parse(classParts[5]);
                    int classIMod = Int32.Parse(classParts[6]);
                    int classID = Int32.Parse(classParts[7]);
                    Class c = new Class(className, classBHP, classHPL, classSMod, classAMod, classCMod, classIMod, new List<Skill>());
                    foreach (Skill k in skillList)
                    {
                        if (k.classID == classID)
                        {
                            Skill x = k.Clone();
                            c.skillList.Add(x);
                        }
                    }
                    classList.Add(c);
                }
            }
        }

        void ReadEncounters()
        {
            Program.ut.TypeLine("Loading encounters");
            string[] encounterLines = System.IO.File.ReadAllLines(Path.Combine(dataFolder, "Encounters.txt"));
            encounterList = new List<Encounter>();
            foreach (string s in encounterLines)
            {
                if (s[0] != '/')
                {
                    string[] encounterParts = s.Split(';');
                    string encName = encounterParts[0];
                    string encIntro = encounterParts[1];
                    if (encounterParts.Length == 4)
                    {
                        string encOuttro = encounterParts[2];
                        encounterList.Add(new Encounter(encName, encIntro, encOuttro, true));
                    }
                    else
                    {
                        string encOuttro = encounterParts[2];
                        string encType = encounterParts[3];
                        int encStr = Int32.Parse(encounterParts[4]);
                        encounterList.Add(new Encounter(encName, encIntro, encOuttro, encType, encStr));
                    }
                }
            }
        }
    }
}
