#include <iostream>
#include <vector>
#include <tuple>
#include <iomanip>
using namespace std;

class point{
    long long x, y;
public:
    point() = default;
    point(long long _x, long long _y){
        x = _x;
        y = _y;
    }
    point(const point  &_point){
        x = _point.x;
        y = _point.y;
    }
    void set_x(long long _x){
        x = _x;
    }
    void set_y(long long _y){
        y = _y;
    }
    long long get_x()const {
        return x;
    }
    long long  get_y() const{
        return y;
    }
    point operator+(const point &point1) const{
        return {x + point1.get_x() , y + point1.get_y()};
    }
    point operator-(const point &point1) const{
        return {x - point1.get_x(), y - point1.get_y()};
    }
    long long orientation_test(const point &P, const point &Q)const{
        return Q.get_x() * y + P.get_x() * Q.get_y() + P.get_y() * x - Q.get_x() * P.get_y() - x * Q.get_y() - y*P.get_x();
    }
    bool is_on_segment(const point &point1, const point &point2)const{
        long long x_max = point1.get_x(), x_min = point2.get_x();
        if(x_max < x_min){
            x_max = point2.get_x();
            x_min = point1.get_x();
        }
        if(this->orientation_test(point1, point2) == 0 && this->x >= x_min && this->x <= x_max)
            return true;
        return false;
    }

};

class semiPlan{
    long long a, b, c;
public:
    semiPlan() = default;
    semiPlan(const long long _a,const long long _b,const long long _c){
        a = _a;
        b = _b;
        c = _c;
    }
    semiPlan(const semiPlan &_semiPlan){
        a = _semiPlan.a;
        b = _semiPlan.b;
        c = _semiPlan.c;
    }
    void set_a(const long long _a){
        a = _a;
    }
    void set_b(const long long _b){
        b = _b;
    }
    void set_c(const long long _c){
        c = _c;
    }
    long long get_a() const{
        return a;
    }
    long long get_b() const{
        return b;
    }
    long long get_c() const{
        return c;
    }
};


/*
 xA yA xA^2 + yA^2      1       xA      yA      xA^2 + y^A^2                1
 xB yB  xB^2 + yB^2     1       xB-xA   yB-yA   xB^2 - xA^2 + yB^2 - yA^2   1
 xC yC  xC^2 + yC^2     1   =
 xD yD  xD^2 + yD^2     1


     (xB-xA)  (yB-yA)   ((xB-xA)(xB+xA) + (yB-yA)(yB+yA))
     (xC-xA)  (yC-yA)   ((xC-xA)(xC+xA) + (yC-yA)(yC+yA))
     (xD-xA)  (yD-yA)   ((xD-xA)(xD+xA) + (yD-yA)(yD+yA))

    (xB-xA)  (yB-yA)   ((xB-xA)(xB+xA) + (yB-yA)(yB+yA))
    (xC-xA)  (yC-yA)   ((xC-xA)(xC+xA) + (yC-yA)(yC+yA))

    =   -((xB-xA) * ((xD-xA)(xD+xA) + (yD-yA)(yD+yA)) * (yC-yA) + (xC-xA) * ((xB-xA)(xB+xA) + (yB-yA)(yB+yA)) * (yD-yA) +
    (xD-xA) * ((xC-xA)(xC+xA) + (yC-yA)(yC+yA)) * (yB-yA) - (yC-yA) * ((xB-xA)(xB+xA) + (yB-yA)(yB+yA)) * (xD-xA) -
    (yD-yA) * ((xC-xA)(xC+xA) + (yC-yA)(yC+yA)) * (xB-xA) - (yB-yA) * ((xD-xA)(xD+xA) + (yD-yA)(yD+yA)) * (xC-xA))

 */

long long criteriuNumeric(const point &A, const point &B, const point &C, const point &D){
    long long xA = A.get_x(), yA = A.get_y(), xB = B.get_x(), yB = B.get_y(), xC = C.get_x(), yC = C.get_y(), xD = D.get_x(), yD = D.get_y();
    return -((xB-xA) * ((xD-xA)*(xD+xA) + (yD-yA)*(yD+yA)) * (yC-yA) + (xC-xA) * ((xB-xA)*(xB+xA) + (yB-yA)*(yB+yA)) * (yD-yA) +
           (xD-xA) * ((xC-xA)*(xC+xA) + (yC-yA)*(yC+yA)) * (yB-yA) - (yC-yA) * ((xB-xA)*(xB+xA) + (yB-yA)*(yB+yA)) * (xD-xA) -
           (yD-yA) * ((xC-xA)*(xC+xA) + (yC-yA)*(yC+yA)) * (xB-xA) - (yB-yA) * ((xD-xA)*(xD+xA) + (yD-yA)*(yD+yA)) * (xC-xA));
}

void position_Point_Circumscised_Triangle(){
    long long x_aux, y_aux;
    point point_aux{};
    vector<point>points;

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point A(point_aux);

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point B(point_aux);

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point C(point_aux);

    int m;
    cin >> m;
    for(int i = 0; i < m; i++){
        cin >> x_aux >> y_aux;
        point_aux.set_x(x_aux);
        point_aux.set_y(y_aux);
        points.push_back(point_aux);
    }
    for(int i = 0; i < m; i++){
        long long aux = criteriuNumeric(A, B, C, points[i]);
        if (aux > 0)
            cout << "INSIDE" << endl;
        else
        if (aux == 0)
            cout << "BOUNDARY" << endl;
        else
            cout << "OUTSIDE" << endl;
    }
}

void muchiiIlegale(){
    long long x_aux, y_aux;
    point point_aux{};
    vector<point>points;

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point A(point_aux);

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point B(point_aux);

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point C(point_aux);

    cin >> x_aux >> y_aux;
    point_aux.set_x(x_aux);
    point_aux.set_y(y_aux);
    point D(point_aux);

    if(criteriuNumeric(A, B, C, D) > 0)
        cout << "AC: ILLEGAL" << endl;
    else
        cout << "AC: LEGAL" << endl;

    if(criteriuNumeric(D, A, B, C) > 0)
        cout << "BD: ILLEGAL" << endl;
    else
        cout << "BD: LEGAL" << endl;

}

void semiPlan_Intersection(){

    int n;
    long long a_aux, b_aux, c_aux;
    vector<semiPlan> horizontal;
    vector<semiPlan> vertical;

    cin >> n;
    if(n == 1) {
        cout << "UNBOUNDED" << endl;
        return ;
    }
    for(int i = 0; i < n; i ++){
        cin >> a_aux >> b_aux >> c_aux;
        semiPlan semiPlan_aux (a_aux, b_aux, c_aux);
        if(a_aux == 0)
            horizontal.push_back(semiPlan_aux);
        else
            vertical.push_back(semiPlan_aux);
    }

    tuple<bool, double> max_x = make_tuple(false, 0), min_x = make_tuple(false, 0),
            max_y = make_tuple(false, 0), min_y = make_tuple(false, 0);

    for(auto & i : horizontal){
        if(i.get_b() > 0) {
            if (!get<0>(max_y)) {
                get<0>(max_y) = true;
                get<1>(max_y) = double(-i.get_c()) / double(i.get_b());
            }
            else
            if(get<1>(max_y) > double(-i.get_c()) / double(i.get_b()))
                get<1>(max_y) = double(-i.get_c()) / double(i.get_b());
        }
        else{
            if (!get<0>(min_y)) {
                get<0>(min_y) = true;
                get<1>(min_y) = double(-i.get_c()) / double(i.get_b());
            }
            else
            if(get<1>(min_y) < double(-i.get_c()) / double(i.get_b()))
                get<1>(min_y) = double(-i.get_c()) / double(i.get_b());
        }
    }

    for(auto & i : vertical){
        if(i.get_a() > 0) {
            if (!get<0>(max_x)) {
                get<0>(max_x) = true;
                get<1>(max_x) = double(-i.get_c()) / double(i.get_a());
            }
            else
            if(get<1>(max_x) > double(-i.get_c()) / double(i.get_a()))
                get<1>(max_x) = double(-i.get_c()) / double(i.get_a());
        }
        else{
            if (!get<0>(min_x)) {
                get<0>(min_x) = true;
                get<1>(min_x) = double(-i.get_c()) / double(i.get_a());
            }
            else
            if(get<1>(min_x) < double(-i.get_c()) / double(i.get_a()))
                get<1>(min_x) = double(-i.get_c()) / double(i.get_a());
        }
    }

    if (get<0>(min_x) && get<0>(max_x)){
        if (get<1>(min_x) > get<1>(max_x)) {
            cout << "VOID" << endl;
            return ;
        }
    }
    if (get<0>(min_y) && get<0>(max_y)){
        if (get<1>(min_y) > get<1>(max_y)) {
            cout << "VOID" << endl;
            return ;
        }
    }
    if ((get<0>(min_x) && get<0>(max_x)) && (get<0>(min_y) && get<0>(max_y))) {
        cout << "BOUNDED" << endl;
        return ;
    }
    cout << "UNBOUNDED" << endl;
}

double getLimit_Y(const semiPlan &semiPlan){
    return double(-semiPlan.get_c()) / double(semiPlan.get_b());
}

double getLimit_X(const semiPlan &semiPlan){
    return double(-semiPlan.get_c()) / double(semiPlan.get_a());
}

//vector<tuple<double, double>> createRectangle(const semiPlan& horizontal1, const semiPlan& horizontal2, const semiPlan& vertical1, const semiPlan& vertical2){
//    vector<tuple<double, double>> returnVector;
//    tuple<double, double> aux_tuple;
//    get<0>(aux_tuple) = getLimit_X(vertical1);
//    get<1>(aux_tuple) = getLimit_Y(horizontal1);
//    returnVector.push_back(aux_tuple);
//    get<0>(aux_tuple) = getLimit_X(vertical1);
//    get<1>(aux_tuple) = getLimit_Y(horizontal2);
//    returnVector.push_back(aux_tuple);
//    get<0>(aux_tuple) = getLimit_X(vertical2);
//    get<1>(aux_tuple) = getLimit_Y(horizontal1);
//    returnVector.push_back(aux_tuple);
//    get<0>(aux_tuple) = getLimit_X(vertical2);
//    get<1>(aux_tuple) = getLimit_Y(horizontal2);
//    returnVector.push_back(aux_tuple);
//    return returnVector;
//}

double calculateArea(const semiPlan& horizontal1, const semiPlan& horizontal2, const semiPlan& vertical1, const semiPlan& vertical2){
    double L = getLimit_Y(horizontal1) - getLimit_Y(horizontal2);
    double l = getLimit_X(vertical1) - getLimit_X(vertical2);
    if(L < 0)
        L = L * (-1);
    if(l < 0)
        l = l * (-1);
    return L*l;
}

bool isBetweenNumbers (double a, double b, double x){
    if(a < b) {
        if (x > a && x < b)
            return true;
        else
            return false;
    }
    else{
        if(x > b && x < a)
            return true;
        else
            return false;
    }
}

int main() {

    int n, m;
    long long a_aux, b_aux, c_aux;
    vector<semiPlan> horizontal;
    vector<semiPlan> vertical;
    double x_aux, y_aux;
    tuple<double, double> tuple_aux;
    vector<tuple<double, double>>points;

    cin >> n;
    for(int i = 0; i < n; i ++){
        cin >> a_aux >> b_aux >> c_aux;
        semiPlan semiPlan_aux (a_aux, b_aux, c_aux);
        if(a_aux == 0)
            horizontal.push_back(semiPlan_aux);
        else
            vertical.push_back(semiPlan_aux);
    }

    cin >> m;
    for(int i = 0; i < m; i++){
        cin >> x_aux >> y_aux;
        tuple_aux = make_tuple(x_aux, y_aux);
        points.push_back(tuple_aux);
    }

    // cream lista de dreptunghiuri interesante

//    vector<vector<int>> x_pairs_index;
//    x_pairs_index.resize(n);
//    vector<vector<int>> y_pairs_index;
//    y_pairs_index.resize(n);
//
//    for(int i = 0; i < horizontal.size() - 1; i++){
//        for(int j = i + 1; j < horizontal.size(); j++){
//            if(horizontal[i].get_b() > 0) { // max value
//                if (horizontal[j].get_b() < 0 && getLimit_Y(horizontal[j]) < getLimit_Y(horizontal[i]))
//                    y_pairs_index[i].push_back(j);
//            }
//            else{
//                if (horizontal[j].get_b() > 0 && getLimit_Y(horizontal[j]) > getLimit_Y(horizontal[i]))
//                    y_pairs_index[i].push_back(j);
//            }
//        }
//    }
//    for(int i = 0; i < vertical.size() - 1; i++){
//        for(int j = i + 1; j < vertical.size(); j++){
//            if(vertical[i].get_a() > 0) { // max value
//                if (vertical[j].get_a() < 0 && getLimit_X(vertical[j]) < getLimit_X(vertical[i]))
//                    x_pairs_index[i].push_back(j);
//            }
//            else{
//                if (vertical[j].get_a() > 0 && getLimit_X(vertical[j]) > getLimit_X(vertical[i]))
//                    x_pairs_index[i].push_back(j);
//            }
//        }
//    }
//    vector<vector<semiPlan>> rectangles;
//    vector<semiPlan> aux;
//    aux.resize(4);
//    for (int i = 0; i < x_pairs_index.size(); i++) {
//        for (int j = 0; j < x_pairs_index[i].size(); j++) {
//            for (int k = 0; k < y_pairs_index.size(); k++) {
//                for (int p = 0; p < y_pairs_index[k].size(); p++) {
//                    aux[0] = vertical[i];
//                    aux[1] = vertical[x_pairs_index[i][j]];
//                    aux[2] = horizontal[k];
//                    aux[3] = horizontal[y_pairs_index[k][p]];
//
//                    rectangles.push_back(aux);
//                }
//            }
//        }
//    }
//
//    for(int u = 0; u < m; u++) {
//        double area = 0;
//        double x = get<0>(points[u]);
//        double y = get<1>(points[u]);
//        for(auto & rectangle : rectangles){
//            double areaRectangle = calculateArea(rectangle[2], rectangle[3],rectangle[0], rectangle[1]);
//            if(isBetweenNumbers(getLimit_X(rectangle[0]), getLimit_X(rectangle[1]), x) &&
//                    isBetweenNumbers(getLimit_Y(rectangle[2]), getLimit_Y(rectangle[3]), y)) {
//                if (areaRectangle < area || area == 0)
//                    area = areaRectangle;
//            }
//        }
//        if(area != 0)
//            cout << "YES" << endl << area << endl;
//        else
//            cout << "NO" << endl;
//    }
    for(int i = 0; i < m; i++){
        tuple<bool, double> x_sup = make_tuple(false, 0);
        tuple<bool, double> x_inf = make_tuple(false, 0);
        tuple<bool, double> y_sup = make_tuple(false, 0);
        tuple<bool, double> y_inf = make_tuple(false, 0);
        for(auto & j : horizontal){
            if(j.get_b() > 0){
                if(getLimit_Y(j) > get<1>(points[i]))
                    if(!get<0>(y_inf) || get<1>(y_inf) > getLimit_Y(j))
                    {
                        get<0>(y_inf) = true;
                        get<1>(y_inf) = getLimit_Y(j);
                    }
            }
            else{
                if(getLimit_Y(j) < get<1>(points[i]))
                    if(!get<0>(y_sup) || get<1>(y_sup) < getLimit_Y(j))
                    {
                        get<0>(y_sup) = true;
                        get<1>(y_sup) = getLimit_Y(j);
                    }
            }
        }
        for(auto & j : vertical){
            if(j.get_a() > 0){
                if(getLimit_X(j) > get<0>(points[i]))
                    if(!get<0>(x_inf) || get<1>(x_inf) > getLimit_X(j))
                    {
                        get<0>(x_inf) = true;
                        get<1>(x_inf) = getLimit_X(j);
                    }
            }
            else{
                if(getLimit_X(j) < get<0>(points[i]))
                    if(!get<0>(x_sup) || get<1>(x_sup) < getLimit_X(j))
                    {
                        get<0>(x_sup) = true;
                        get<1>(x_sup) = getLimit_X(j);
                    }
            }
        }
        if(get<0>(y_inf) && get<0>(y_sup) && get<0>(x_inf) && get<0>(x_sup)){
            cout << "YES" << endl;
            double L = get<1>(y_sup) - get<1>(y_inf);
            double l = get<1>(x_sup) - get<1>(x_inf);
            if(L < 0)
                L = L * (-1);
            if(l < 0)
                l = l * (-1);
            cout << fixed << setprecision(6) << L*l << endl;
        }
        else
            cout << "NO" << endl;
    }
    return 0;
}
