#include <iostream>
#include <vector>
#include "DragonClass.hpp"
#include "EnemyClass.hpp"
#include "NinjaClass.hpp"
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

// Smart pointer - container for a raw container
//      -> they deallocate memory automatically
//      -> 3 pointers: unique, shared and weak
//      -> #include <memory>
//  unique: to a certain addres there can only be assigned 1 unique pointer
//  smart: there is a counter that indicates how many pointers are assigned to a certain address
//  weak: when there is only weak pointers assigned to a addres ( no strong pointers ) the memory will deallocate

// Upcasting & Downcastig
// Base class = b, Derived class = d
// Upcasting : Base *ptr = &derived; ( implicity type cast allowed )
// Downcasting : Only allowed with explicit casting
// Because the is-a relationship ( public inheritance ) is not, in most cases, symmetric

// Exception handling:
//When you throw an exception, the execution of the function ends
//try 
//  ...
//catch (typeOfException exception_name){
//      print exception
//}
//You can write multiple catch blocks for 1 try block
//Default handler can handle any type of exception
//catch(...){
//  print something
//}
// Default handler needs to be the last handler.

int main(){
    Dragon dragon(100, 10, 5, "default");
    cout << dragon;
    Ninja ninja(100, 10, 5, 4, "John");
    cout << ninja << endl << ninja << endl;
    ninja.Attack();
    dragon.Move();
    Ninja ninja1 = ninja;
    ninja1.Move();
    return 0;
}