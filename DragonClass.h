#ifndef DragonClass.H
#define DragonClass.H
#include <EnemyClass.h>
#include <iostream>
#include <string>

using namespace std;

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

#endif