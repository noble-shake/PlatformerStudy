using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Study240130 : MonoBehaviour
{
    /*
     * 2024-01-29 unity class
     * [PREVIOUS] Class
     * [LEARNED] Constructor / Destructor
     * [LEARNED] Inheritance ~ child can access own parent, parent can't see owns inheritances.
     * [LEARNED] virtual / override, update and awake ... can be virtualized with protected priority. ~ it can be used inheritance reproduction even MonoBehaviour's child
     * [PROBLEM] 
     * 
     */

    [SerializeField] public MonsterExternalScript externalMonster;
    [SerializeField] private Monster pMonster;
    [SerializeField] private Honster hMonster;
    [SerializeField] private HonsterChild1 hMonsterChild;
    private int pint = 100;

    [System.Serializable]
    public class Monster {
        // protected ~ child accessable
        public float Hp;
        public float Damage;
        public float Defence;
        public float Speed;
        public int Level;

        public Monster()
        {  // constructor using overload
            Hp = 100;
            Damage = 100;
        }

        public Monster(float _HP, float _Damage, float _Defence) {  // constructor using overload
            Hp = _HP;
            Damage += _Damage;
        }

        public void SetData(float _Hp, float _damage, float _defences) {
            Hp = _Hp;
            Damage = _damage;
            Defence = _defences;
        }

        ~Monster() {  // Destructor
            Debug.Log("Destructed");
        }
    }

    public class highOrc : Monster {
        private float Hp; // these warning explain, if parent's variable initialize again, parent's variable can't use.
        private float SdChance;
    }

    public class  slime : Monster
    {
            private float squidRatio;

    }

    [System.Serializable]
    public class Honster {
        public float Hp;
        public virtual void ShowDebug() {
            Debug.Log("this is parent");
        }
    }
    [System.Serializable]
    public class HonsterChild1 : Honster
    {
        private float Hp;
        public override void ShowDebug() {
            Debug.Log("this is child1");
        }    

    }

    private void Start()
    {
        Monster Parent = new Monster();

        Honster ParentH = new Honster();
        HonsterChild1 ChildH = new HonsterChild1();
        ParentH.ShowDebug();
        ChildH.ShowDebug();

        // List Remind
        List<Honster> classList = new List<Honster>();
        Honster orc = new Honster();
        // [ERROR] classList[0].Hp = 100;
        // it is not variable, used as method.

        // tuple,struct < class
        var value1 = getValue();

        // List of List -> class
        List<List<Honster>> instanceInstanceList = new List<List<Honster>>();

        // Class, CBR -> Heap Memory
        // CBV -> Stack Memory
        // struct can use new keyword but, still in Stack Memory. and can't inheritance
        // CBR use 4Byte, CBV copied all data then, will different.
    }
    public (float, float) getValue() {
        return (0.1f, 0.2f);
    }
    public class getValueClass {
        public float value1;
        public float value2;

        public (float, float) getValueFunction() {
            return (value1, value2);
        }
    }

    protected virtual void Update() { }
}