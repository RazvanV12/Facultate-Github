#ifndef NINJACLASS_H
#define NINJACLASS_H

#include "EnemyClass.hpp"
#include <string>
#include <iostream>

using namespace std;

class Ninja : public Enemy{
    int teacherName_Size;
    char* teacherName;

public:
    Ninja();
    Ninja(float _hp, float _movementSpeed, float _damage, int _teacherName_Size, string _teacherName);
    ~Ninja();
    void Attack();
    void Move();
    friend ostream& operator<<(ostream & COUT, Ninja &ninja);
};

#endif