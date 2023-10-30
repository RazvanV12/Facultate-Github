#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

// Leetcode Problems:

// 80. Remove Duplicates from Sorted Array II
// [1,1,1,2,2,3]
// Trecem prin tot vectorul, cand dam de un numar nou verificam de cate ori apare si mutam elementele la stg
int removeDuplicates(vector<int> nums){
    unsigned long long size = nums.size();
    unsigned int i, j, counter, k = size, m;
    int value;
    for(i = 0; i < k; i++){
        counter = 0;
        value = nums[i];
        j = i;
        while(j < k && nums[j] == value){
            j++;
            counter++;
        }
        if(counter > 2){
            for(j = i + 2; j < k; j++){
                if(j + counter - 2 < k)
                    nums[j] = nums[j + counter - 2];
            }
            i ++;
            k -= (counter - 2);
        }
    }
    return k;
}

//189. Rotate Array
// [1, 2, 3, 4, 5, 6, 7]
// Varianta 1: Rotim de k ori la dreapta cu 1 pozitie tot vectorul - O(n * k) time complexity, O(1) extra space
void rotate1(vector<int>& nums, int k) {
        int aux, temp1, temp2;
        unsigned long long size = nums.size();
        for(unsigned int i = 0; i < k; i++){
            aux = nums[size - 1];
            temp1 = nums[0];
            for(unsigned int j = 1; j < size; j++){
                temp2 = nums[j];
                nums[j] = temp1;
                temp1 = temp2;
            }
            nums[0] = aux;
        }
}

// Varianta 2: Copiem vectorul si trecem direct elementele de pe pozitia i la pozitia (i + k) % size - O(n) time complexity, O(n) extra space
void rotate2(vector<int>& nums, int k) {
        vector<int> copy = nums;
        unsigned long long size = nums.size();
        for(unsigned int i = 0; i < size; i++){
            nums[(i + k) % size] = copy[i];
        }
}

// 122. Best Time to Buy and Sell Stock II
// [7, 1, 5, 3 ,6, 4]
// Calculam fiecare diferenta prices[(i+1)] - prices[i] si o adaugam la profit doar daca este pozitiva
int maxProfit(vector<int> & prices){
    unsigned long long size = prices.size();
    unsigned int maxP = 0;
    for(unsigned int i = 0; i < size - 1; i++){
        if(prices[i + 1] - prices[i] > 0)
            maxP += prices[i + 1] - prices[i];
    }
    return maxP;
}

//55. Jump game
// [2, 3, 1, 1, 4]
// Ideea se rezuma la a pleca de la ultima pozitie si a gasi cea mai din stanga pozitie din care putem ajunge la capatul vectorului ( o notam X ), dupa care reapelam functia 
// pentru un vector copie cu ultima pozitie fiind X
bool canJump(vector<int>& nums) {
        if(nums.size() == 1)
            return true;

        unsigned long long lastIndex = nums.size() - 1;
        unsigned int bestPosition = nums.size() - 1;
        for(int k = lastIndex - 1; k >= 0; k--){
            if(nums[k] > lastIndex - k - 1)
                bestPosition = k;
        }

        if(bestPosition == nums.size() - 1)
            return false;

        vector<int> copy;
        for(unsigned int i = 0; i <= bestPosition; i++){
            copy.push_back(nums[i]);
        }

        return (canJump(copy));
    }

//45. Jump Game II
// [2, 3, 1, 1, 4]
// Ideea se rezuma la a cauta in urmatoarele nums[0] elemente pozitia care te duce cel mai departe, dupa care aceea devine noua "prima pozitie" si tot asa..
int jump(vector<int>& nums) {
    int firstIndex = 0, counter = 1, newFirstIndex;
    unsigned long long size = nums.size();
    if(size == 1)
        return 0;
    while(nums[firstIndex] < size - firstIndex - 1){
        newFirstIndex = firstIndex + 1;
        for(int i = firstIndex + 2; i < size && i < nums[firstIndex] + firstIndex + 1; i++){
            if(nums[i] > nums[newFirstIndex] - (i - newFirstIndex)){
                newFirstIndex = i;
            }
        }
        firstIndex = newFirstIndex;
        counter++;
    }
    return counter;
}

// 274. H-Index
// [3, 0, 6, 1, 5]
// Sortam vectorul ( in O(nlogn) ) dupa care mergem cu un for de la 0 la n si verificam daca vector[i] > n- i returnam i
int hIndex(vector<int> & citations){
    sort(citations.begin(), citations.end());
    int size = citations.size();
    for(int i = 0; i < size; i++){
        if(citations[i] >= size - i)
            return (size - i);
    }
    return 0;
}



int main(){
    vector<int> nums = {2, 0, 2, 0 , 1};
    cout << jump(nums);
    
    return 0;
}