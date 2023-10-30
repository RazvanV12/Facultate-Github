#ifndef DRAGONCLASS_H
#define DRAGONCLASS_H

#include "EnemyClass.hpp"
#include <string>
#include <iostream>

using namespace std;

class Dragon : public Enemy{
    string skin;
public:
    void Attack();
    void Move();
    Dragon();
    Dragon(float _hp, float _movementSpeed, float _damage, string _skin);
    Dragon(float _movementSpeed, float _damage, string _skin);
    friend ostream& operator<<(ostream &COUT, Dragon &dragon);
};

#endif