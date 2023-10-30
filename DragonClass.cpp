#include "DragonClass.hpp"
#include <iostream>

void Dragon::Attack(){
    cout << "Dragon attacks" << endl;
}

void Dragon::Move(){
    cout << "Dragon moves at " << movementSpeed << " speed" << endl;
}
Dragon::Dragon(){};
Dragon::Dragon(float _hp, float _movementSpeed, float _damage, string _skin) : Enemy(_hp, _movementSpeed, _damage){
    skin = _skin;
}
Dragon::Dragon(float _movementSpeed, float _damage, string _skin) : Enemy(_movementSpeed, _damage){
    skin = "default";
}
ostream& operator<<(ostream &COUT, Dragon &dragon){
    COUT << dragon.skin << endl;
    return COUT;
}