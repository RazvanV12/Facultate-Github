#include <iostream>
#include <string>

using namespace std;

void reverseWords(string &sentence){
    int n = sentence.size(), j, stg, dr;
    char aux;
    for(int i = 0; i < n; i++){
        if(sentence[i] != ' ')
        {
            j = i;
            while(sentence[j] != ' ' && j < sentence.size()){
                j++;
            }
            stg = i;
            dr = j - 1;
            while(stg < dr){
                aux = sentence[stg];
                sentence[stg] = sentence[dr];
                sentence[dr] = aux;
                stg++;
                dr--;
            }
            i = j;
        }
    }
}

void reverseSentence(string &sentence){
    int n sentence.size();

}

int main(){
    string sentence = "   Acesta   este un    test";
    reverseWords(sentence);
    cout << sentence;
}