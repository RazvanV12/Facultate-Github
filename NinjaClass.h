#ifndef NinjaClass.H
#define NinjaClass.H

#include <EnemyClass.h>
#include <string>
#include <iostream>

using namespace std;

class Ninja : public Enemy{
    int teacherName_Size;
    char* teacherName;

private:
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
    }
    void Attack(){
        cout << "Ninja attack" << endl;
    }
    void Move(){
        cout << "Ninja moves at " << movementSpeed << " speed ";
    }

};

#endif