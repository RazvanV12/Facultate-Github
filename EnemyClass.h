#ifndef EnemyClass_H
#define EnemyClass_H

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

#endif