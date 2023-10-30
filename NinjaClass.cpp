#include "NinjaClass.hpp"

Ninja::Ninja(){};
Ninja::Ninja(float _hp, float _movementSpeed, float _damage, int _teacherName_Size, string _teacherName) : Enemy(_hp, _movementSpeed, _damage){
    teacherName_Size = _teacherName_Size;
    teacherName = new char [teacherName_Size];
    for(int i = 0; i < _teacherName_Size; i++){
        teacherName[i] = _teacherName[i];
    }
}
Ninja::~Ninja(){
    delete [] teacherName;
    teacherName = nullptr;
}
void Ninja::Attack(){
    cout << "Ninja attack" << endl;
}
void Ninja::Move(){
    cout << "Ninja moves at " << movementSpeed << " speed ";
}

ostream& operator<< (ostream &COUT, Ninja &ninja){
    for(int i = 0; i < ninja.teacherName_Size; i++){
        COUT << ninja.teacherName[i];
    }
    return COUT;
}
