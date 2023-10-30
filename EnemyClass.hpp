#ifndef ENEMYCLASS_H
#define ENEMYCLASS_H

class Enemy{
protected:
    float hp;
    float movementSpeed;
    float damage;

public:
    float getHp();
    void setHp(float newHp);
    float getMovementSpeed();
    void setMovementSpeed(float newMovementSpeed);
    float getDamage();
    void setDamage(float newDamage);
    virtual void Move() = 0;
    virtual void Attack() = 0;
    Enemy();
    Enemy(float _movementSpeed, float _damage);
    Enemy(float _hp, float _movementSpeed, float _damage);
    Enemy(Enemy & original);
    bool operator==(Enemy const &other)const ;
};

#endif