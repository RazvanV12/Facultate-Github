#include <iostream>
#include <vector>
// #include "E:\Facultate\Facultate-Github\DragonClass.h"
// #include "E:\Facultate\Facultate-Github\DragonClass.h"
// #include "E:\Facultate\Facultate-Github\NinjaClass.h"

using namespace std;

// OOP Practice:

// Encapsulation: 
// - este realizata atunci cand membrii privati ai unei clase sunt accesati doar prin intermediul membrilor publici ai clasei respective

// Inheritance:
// - este realizata atunci cand o clasa mosteneste proprietatile altei clase

// Polymorphism:
// - este realizata atunci cand o clasa mosteneste proprietatile altei clase si le modifica

// Abstraction:
// - este realizata atunci cand o clasa ascunde detalii de implementare


// Exemplu de aplicatie care foloseste toate cele 4 concepte cu ajutorul a 5 clase:

// Vom avea clasa de baza Enemy abstracta, care va fi mostenita de 2 clase: dragon si ninja

class Enemy{
protected:
    float hp;
    float movementSpeed;
    float damage;

public:
    float getHp(){
        return hp;
    }
    void setHp(float newHp){
        hp = newHp;
    }
    float getMovementSpeed(){
        return movementSpeed;
    }
    void setMovementSpeed(float newMovementSpeed){
        movementSpeed = newMovementSpeed;
    }
    float getDamage(){
        return damage;
    }
    void setDamage(float newDamage){
        damage = newDamage;
    }
    virtual void Move() = 0;
    virtual void Attack() = 0;
    Enemy(){};
    Enemy(float _movementSpeed, float _damage){
        hp = 100;
        movementSpeed = _movementSpeed;
        damage = _damage;
    }
    Enemy(float _hp, float _movementSpeed, float _damage){
        hp = _hp;
        movementSpeed = _movementSpeed;
        damage = _damage;
    }
    Enemy(Enemy & original){
        hp = original.hp;
        movementSpeed = original.movementSpeed;
        damage = original.damage;
    }
    bool operator==(Enemy const &other){
        if(this->hp == other.hp && this->movementSpeed == other.movementSpeed && this->damage == other.damage)  
            return true;
        return false;
    }
};
class Dragon : public Enemy{
    string skin;
public:
    void Attack(){
        cout << "Dragon attacks" << endl;
    }

    void Move(){
        cout << "Dragon moves at " << movementSpeed << " speed" << endl;
    }
    Dragon(){};
    Dragon(float _hp, float _movementSpeed, float _damage, string _skin) : Enemy(_hp, _movementSpeed, _damage){
        skin = _skin;
    }
    Dragon(float _movementSpeed, float _damage, string _skin) : Enemy(_movementSpeed, _damage){
        skin = "default";
    }
    ostream& operator<<(ostream &COUT){
        COUT << skin << endl;
        return COUT;
    }
};

class Ninja : public Enemy{
    int teacherName_Size;
    char* teacherName;

public:
    Ninja(){};
    Ninja(float _hp, float _movementSpeed, float _damage, int _teacherName_Size, string _teacherName) : Enemy(_hp, _movementSpeed, _damage){
        teacherName_Size = _teacherName_Size;
        teacherName = new char [teacherName_Size];
        for(int i = 0; i < _teacherName_Size; i++){
            teacherName[i] = _teacherName[i];
        }
    }
    ~Ninja(){
        delete [] teacherName;
        teacherName = nullptr;
        cout << "Destructor called "<< endl;
    }
    Ninja(Ninja &original){
        this->hp = original.hp;
        this->movementSpeed = original.movementSpeed;
        damage = original.damage;
        teacherName_Size = original.teacherName_Size;
        teacherName = new char[teacherName_Size];
        for(int i = 0; i < teacherName_Size; i++){
            teacherName[i] = original.teacherName[i];
        }
        cout << "Copy constructor called " << endl;
    }
    void Attack(){
        cout << "Ninja attack" << endl;
    }
    void Move(){
        cout << "Ninja moves at " << movementSpeed << " speed " << endl;
    }

    friend ostream& operator<<(ostream & COUT, Ninja &ninja);
};

ostream& operator<< (ostream &COUT, Ninja &ninja){
    for(int i = 0; i < ninja.teacherName_Size; i++){
        COUT << ninja.teacherName[i];
    }
    return COUT;
}

int main(){
    Dragon dragon(100, 10, 5, "default");
    dragon.operator<<(cout);
    Ninja ninja(100, 10, 5, 4, "John");
    cout << ninja << endl << ninja << endl;
    ninja.Attack();
    dragon.Move();
    Ninja ninja1 = ninja;
    ninja1.Move();
    return 0;
}