#include "EnemyClass.hpp"

float Enemy::getHp(){
    return hp;
}
void Enemy::setHp(float newHp){
    hp = newHp;
}
float Enemy::getMovementSpeed(){
    return movementSpeed;
}
void Enemy::setMovementSpeed(float newMovementSpeed){
    movementSpeed = newMovementSpeed;
}
float Enemy::getDamage(){
    return damage;
}
void Enemy::setDamage(float newDamage){
    damage = newDamage;
}
Enemy::Enemy(){};
Enemy::Enemy(float _movementSpeed, float _damage){
    hp = 100;
    movementSpeed = _movementSpeed;
    damage = _damage;
}
Enemy::Enemy(float _hp, float _movementSpeed, float _damage){
    hp = _hp;
    movementSpeed = _movementSpeed;
    damage = _damage;
}
Enemy::Enemy(Enemy & original){
    hp = original.hp;
    movementSpeed = original.movementSpeed;
    damage = original.damage;
}
bool Enemy::operator==(Enemy const&other) const{
    if(this->hp == other.hp && this->movementSpeed == other.movementSpeed && this->damage == other.damage)  
        return true;
    return false;
}